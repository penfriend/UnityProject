using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        //Level.current.addCoins(1);
        //this.CollectedHide();
        this.CollectedHide();
    }
}
