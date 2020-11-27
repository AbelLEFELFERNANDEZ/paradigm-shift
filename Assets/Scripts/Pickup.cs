using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickup : Resetteable
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        Picked();

    }

    protected abstract void Picked();

    public override void Reset()
    {
        gameObject.SetActive(true);
    }

}
