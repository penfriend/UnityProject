using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    float time_to_wait = 5f;
    Vector3 startingPosition;
    Quaternion startingRotation;
    float healthNumber;
    bool isBigRabbit = false;
    bool isDead = false;
    public static LevelController current;
    void Awake()
    {
        current = this;
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
        if (this.isBiggerRabbit())
        {
            this.scaleSmallerRabit(rabit);
        }
        else
        {
            
                //Повідомляємо рівень, про смерть кролика
                //При смерті кролика повертаємо на початкову позицію
                rabit.transform.position = this.startingPosition;
                rabit.transform.rotation = this.startingRotation;
               
           
        }
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
        rabit.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
        this.isBigRabbit = true;
       
    }
    public void scaleSmallerRabit(HeroRabbit rabit)
    {
        rabit.transform.localScale -= new Vector3(0.5F, 0.5F, 0.5F);
        this.isBigRabbit = false;

    }

}
