using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    protected override void Picked()
    {
        GameController.instance.GetKey();
        gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().Play("KeyGet");
    }

    public override void Reset()
    {

    }
}
