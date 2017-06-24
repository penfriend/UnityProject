using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool bomb = false;
    float time_to_wait = 5f;
    Vector3 startingPosition;
    Quaternion startingRotation;
    float healthNumber;
    bool isBigRabbit = false;
    bool isDead = false;
    public static LevelController current;
    bool deathZone = false;
    void Awake()
    {
        current = this;
    }
   public bool DeathZone()
    {
        this.deathZone = !deathZone;
        return this.deathZone;
    }
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void setStartRotation(Quaternion rotat)
    {
        this.startingRotation = rotat;
    }
    public void setHealthNumber(float number)
    {
        this.healthNumber = number;
    }
    
    public void onRabitDeath(HeroRabbit rabit)
    {

        if (this.isBiggerRabbit() )
        {
            this.scaleSmallerRabit(rabit);
            if(this.deathZone){
                this.isDead = true; StartCoroutine(rabbitDie(rabit));
            

         
            }
        }
        else
        {
            StartCoroutine(rabbitDie(rabit));
          this.isDead = true;
            
        }
    }
    IEnumerator rabbitDie(HeroRabbit rabit)
    {
        rabit.isDead=true;
        yield return new WaitForSeconds(1);
        //Повідомляємо рівень, про смерть кролика
        //При смерті кролика повертаємо на початкову позицію
        rabit.isDead = false;
        rabit.transform.position = this.startingPosition;
        rabit.transform.rotation = this.startingRotation;
        
    }
    void Update() { this.isDead = false;  this.deathZone = false;
    }
    public bool isBiggerRabbit()
    {
        return this.isBigRabbit;
    }
    public bool isRabbitDead()
    {
        return this.isDead;
    }
    public void scaleRabit(HeroRabbit rabit)
    {
        if(!this.isBigRabbit){
        rabit.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
        this.isBigRabbit = true;}
       
    }
    public void scaleSmallerRabit(HeroRabbit rabit)
    {
    
        rabit.transform.localScale -= new Vector3(0.5F, 0.5F, 0.5F);
        this.isBigRabbit = false;}

    }


