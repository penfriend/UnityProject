using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
    public float Speed = 2f;
    public float Time_to_wait = 4.5f;
    float time_to_wait = 4.5f;
    float my_direction;
    void Start()
    {
        StartCoroutine(destroyLater());
    }
    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

	protected override void OnRabitHit(HeroRabbit rabit)
    {
        this.CollectedHide();
        LevelController.current.onRabitDeath(rabit);
    }
    //запускає моркву або в одну або в іншу сторону
    public void launch(float direction)
    {
        this.my_direction = direction;
        time_to_wait -= Time.deltaTime;
        if (time_to_wait <= 0)
        {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            if (direction > 0)
            {
                sr.flipX = true;
            }
            else if (direction < 0)
            {
                sr.flipX = false;
            }

        } time_to_wait = this.Time_to_wait;
    }
    void Update()
    {
        Vector3 my_pos = this.transform.position;
        Quaternion my_rot = this.transform.rotation;
        if (my_direction > 0)
        {
            
            my_pos.x -= Time.deltaTime * this.my_direction * this.Speed;
            my_rot.z = 180f;
        }
        else
        {
            
            my_pos.x -= Time.deltaTime * this.my_direction * this.Speed;
        }
        this.transform.position = my_pos;
        this.transform.rotation = my_rot;
    }
}
