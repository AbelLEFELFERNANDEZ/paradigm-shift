using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    protected override void Picked()
    {
        GameController.instance.GetKey();
    }
}
