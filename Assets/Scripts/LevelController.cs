using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    /* Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
		
    }*/
    Vector3 startingPosition;
    public static LevelController current;
    void Awake()
    {
        current = this;
    }
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabbit rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
    }

}
