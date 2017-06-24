using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrk : MonoBehaviour
{

    public Vector3 pointA; //!!!
    public Vector3 pointB;  //!!!
    float Speed = 1;
    bool going_to_a = true;
    bool going_to_rabbit = false;
    Rigidbody2D myBody = null;  //1
  
    //new for Orc
    Animator greenOrkController = null; //3

   
    Mode mode = Mode.GoToB;
	// Use this for initialization
   
    public enum Mode {
        GoToA,
        GoToB,
        Attack
    }
   
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        greenOrkController = this.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float value = this.getDirection ();
  
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
       
        }

      

    float getDirection()
    {
        Vector3 my_pos = this.transform.position;
        Vector3 target = this.pointB; 
        //Тепер будь де можна дізнатися позицію кролика
        Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;
        //Перевірка чи кролик зайшов в зону патрулювання
        if (rabit_pos.x > Mathf.Min(this.pointA.x, this.pointB.x)
        && rabit_pos.x < Mathf.Max(this.pointA.x, this.pointB.x))
        {
            mode = Mode.Attack;
            this.going_to_rabbit = true;
            if (Vector3.Distance(rabit_pos, this.pointB) > Vector3.Distance(rabit_pos, this.pointA)) { target = this.pointA; }
            else{ target = this.pointB;}
        }
        else { this.going_to_rabbit = false; }

        if (this.going_to_a && !this.going_to_rabbit)
        {
            target = this.pointA;
            this.mode = Mode.GoToB;

        }
        else if (!this.going_to_rabbit)
        {
            target = this.pointB;
            this.mode = Mode.GoToA;
        }
       

        if (isArrived(target))
        {
            this.going_to_a = !going_to_a;
        }
        else
        {
            Vector3 destination = target - my_pos;
            destination.z = 0;
            float move = this.Speed * Time.deltaTime;
            float distance = Vector3.Distance(destination, my_pos);
            Vector3 move_vec = destination.normalized * Mathf.Min(move, distance);
            this.transform.position += move_vec;
        }
        
        if (mode == Mode.Attack)
        {
            //Move towards rabit
            if (my_pos.x < rabit_pos.x)
            {

                return -1;
            }
            else
            {
                return 1;
            }
        }
        if (this.mode == Mode.GoToB)
        {
            return -1; //Move left
        }
        else if (this.mode == Mode.GoToA)
        {
            return 1; //Move right
        }
        return 0; //No movement
    }

    void isDead() { this.greenOrkController.SetBool("death", true); }
    void isAttack() { this.greenOrkController.SetTrigger("attack"); }
    bool isArrived(Vector3 target)
    {
        target.z = 0;
        return Vector3.Distance(this.transform.position, target) < 0.02f;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Rabbit")
        {
            Vector3 v = (Vector3)coll.gameObject.transform.position;
            HeroRabbit rabbitCollision = coll.gameObject.GetComponent<HeroRabbit>();
            if (v.y > this.transform.position.y) { StartCoroutine(orcDie()); }
            else
            {
                
                StartCoroutine(orcAttack( rabbitCollision));
                 }
        }
    }
    IEnumerator orcDie()
    {
        this.isDead();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    IEnumerator orcAttack(HeroRabbit rabbitCollision)
    {
        this.isAttack();
        yield return new WaitForSeconds(1); 
        LevelController.current.onRabitDeath(rabbitCollision);
    }

}
