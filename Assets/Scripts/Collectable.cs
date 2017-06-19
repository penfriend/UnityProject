using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected virtual void OnRabitHit(HeroRabbit rabbit)
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
            //HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        HeroRabbit rabbit = collider.gameObject.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                this.OnRabitHit(rabbit);
            }
    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}
