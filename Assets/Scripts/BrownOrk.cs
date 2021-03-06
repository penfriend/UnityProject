﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrk : MonoBehaviour {

    public float patrolRadius = 4f;
    Vector3 pointA; //!!!
    Vector3 pointB;  //!!!
    float MoveBy=3f;
    float Speed = 1;
    bool going_to_a = true;
    bool going_to_rabbit = false;
    Rigidbody2D myBody = null;  //1
       //new for Orc
    Animator brownOrkController = null; //3
    //Prefab з якого будуть копії
    public GameObject prefabCarrot;
    float last_carrot = 5f;
    //Тепер будь де можна дізнатися позицію кролика
    Vector3 rabit_pos ;

    public enum Mode {
        GoToA,
        GoToB,
        Attack
    }
    Mode mode = Mode.GoToB;
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        brownOrkController = this.GetComponent<Animator>();
      //  this.pointA = this.transform.position;
        //this.pointB = this.pointA + MoveBy;
      //  this.pointB = this.pointA + this.MoveBy;
       // this.pointA = new Vector3(28.03f, 2.03f, 0);
        this.pointA = this.transform.position;
       this.pointB = this.pointA;

      //  this.pointB = new Vector3(23f, 2.03f, 0);
        this.pointB.x = this.pointA.x - this.MoveBy;
    }

    // Update is called once per frame
    void Update()
    {
        this.rabit_pos = HeroRabbit.lastRabit.transform.position;
        float value = this.getDirection ();
        if (Mathf.Abs(rabit_pos.x - this.transform.position.x) < this.patrolRadius)
        {
            this.launchCarrot(value);
       }
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
        Vector3 targetCarrot = this.pointB; 
        
        //Перевірка чи кролик зайшов в зону патрулювання
        if (Mathf.Abs(rabit_pos.x  - this.transform.position.x) < this.patrolRadius)
        {            
            
            mode = Mode.Attack;
            this.going_to_rabbit = true;
            this.isAttack();
            if (Vector3.Distance(rabit_pos, this.pointB) > Vector3.Distance(rabit_pos, this.pointA)) { targetCarrot = this.pointA; }
            else { targetCarrot = this.pointB; }
        }
        else
        {
            this.going_to_rabbit = false; 
        
        }

        if (this.going_to_a && !this.going_to_rabbit)
        {
            this.isRun();
            target = this.pointA;
            this.mode = Mode.GoToB;

        }
        else if (!this.going_to_rabbit)
        {
           this.isRun();
            target = this.pointB;
            this.mode = Mode.GoToA;
        }


        if (isArrived(target) && (!this.going_to_rabbit))
        {
            this.going_to_a = !going_to_a;
        }
        else if (!this.going_to_rabbit)
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

    void isRun() { this.brownOrkController.SetBool("run", true); this.brownOrkController.SetBool("attack", false); }
    void isDead() { this.brownOrkController.SetBool("death", true);  }
    void isAttack() { this.brownOrkController.SetBool("attack", true); this.brownOrkController.SetBool("run", false); }
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

            if (v.y > this.transform.position.y) { this.brownOrkController.SetBool("run", false); this.brownOrkController.SetBool("attack", false); StartCoroutine(orcDie()); }
            
                
                
                 
        }
    }
    IEnumerator orcDie()
    {
        this.isDead();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    IEnumerator orcAttack()
    {
        this.isAttack();
        yield return new WaitForSeconds(1); 
    }

    void launchCarrot(float direction)
    {
        //check launch time
        if (Time.time - this.last_carrot > 2.0f)
        {
             //fix the time of last launch
            this.last_carrot = Time.time;
        
        //Створюємо копію Prefab
            GameObject obj = GameObject.Instantiate(this.prefabCarrot); 
        if (this.transform.position.x < rabit_pos.x) { obj.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.5f); }
        //Розміщуємо в просторі
            
        else { obj.transform.position = new Vector3(this.transform.position.x + 0.3f, this.transform.position.y + 0.4f); }
        //Запускаємо в рух
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
        }
    }

	}

