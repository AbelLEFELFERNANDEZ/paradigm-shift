using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicLaser : MonoBehaviour
{

    public GameObject laserShot;

    public float chargeTime = 2f;
    public float onTime = 1f;

    private void Start()
    {
        StartCoroutine(ChargeUp());
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

}
