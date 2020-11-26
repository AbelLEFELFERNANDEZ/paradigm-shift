using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    AudioSource Audio;


    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() && GameController.instance.hasKey)
        {
            Audio.Play();
            GameController.instance.Win();
        }
    }
}
