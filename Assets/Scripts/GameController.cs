using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController instance;

    private void Awake()
    {
        instance = this;
    }

}
