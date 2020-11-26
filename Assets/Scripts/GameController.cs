using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public AudioSource coinSource;
    public AudioSource keySource;

    public int coins = 0;
    public bool hasKey = false;

    private Resetteable[] resetObjects;

    private void Awake()
    {
        instance = this;
        coins = 0;
        hasKey = false;
    }

    private void Start()
    {
        resetObjects = Object.FindObjectsOfType<Resetteable>();
    }

    public void Reset()
    {
        coins = 0;
        hasKey = false;

        foreach(Resetteable obj in resetObjects)
        {
            obj.Reset();
        }
    }

    public void GainCoin()
    {
        if (coinSource)
        {
            coinSource.Play();
        }
        coins += 1;
    }

    public void GetKey()
    {
        hasKey = true;
        if (keySource)
        {
            keySource.Play();
        }
    }

    public void Win()
    {
        Debug.Log("yay");
    }

}
