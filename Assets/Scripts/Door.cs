using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
     public AudioSource Opening;
     public AudioSource Win;

    private void Start()
    {
        //Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() && GameController.instance.hasKey)
        {
            Win.Play();
            Opening.Play();
            transform.Find("Lock").gameObject.SetActive(false);
            GetComponent<Animator>().Play("DoorOpen");
            GameController.instance.Win();
        }
    }
}
