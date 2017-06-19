using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        //Level.current.addCoins(1);
        //this.CollectedHide();
        this.CollectedHide();
        LevelController.current.scaleRabit(rabit);
    }
}
