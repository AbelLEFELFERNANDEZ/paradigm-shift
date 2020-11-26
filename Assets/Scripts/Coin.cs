using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup
{

    protected override void Picked()
    {
        GameController.instance.GainCoin();
    }
}
