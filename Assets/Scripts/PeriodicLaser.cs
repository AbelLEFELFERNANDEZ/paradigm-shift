using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PeriodicLaser : Resetteable
{

    public GameObject laserShot;

    public float chargeTime = 2f;
    public float onTime = 1f;
    private float currentTime = 0f;

    private void Update()
    {
        if (laserShot.activeSelf)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= onTime)
            {
                currentTime = 0;
                laserShot.SetActive(false);
            }
        } else
        {
            currentTime += Time.deltaTime;
            if (currentTime >= chargeTime)
            {
                currentTime = 0;
                laserShot.SetActive(true);
            }
        }
    }

    IEnumerator ChargeUp()
    {
        yield return new WaitForSeconds(chargeTime);
        laserShot.SetActive(true);
        StartCoroutine(LaserOn());
    }

    IEnumerator LaserOn()
    {
        yield return new WaitForSeconds(onTime);
        laserShot.SetActive(false);
        StartCoroutine(ChargeUp());
    }

    public override void Reset()
    {
        laserShot.SetActive(false);
        currentTime = 0;
    }
}
