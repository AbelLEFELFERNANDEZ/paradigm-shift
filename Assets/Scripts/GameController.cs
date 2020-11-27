using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public AudioSource coinSource;
    public AudioSource keySource;

    public Text scoreText;


    public bool hasKey = false;

    public GameObject endLevelPanel;

    private Resetteable[] resetObjects;

    private void Awake()
    {
        PlayerPrefs.SetInt("CoinScore", 0);
        instance = this;
        hasKey = false;
    }

    private void Start()
    {
        resetObjects = Object.FindObjectsOfType<Resetteable>();
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("CoinScore", 0);
        scoreText.text = PlayerPrefs.GetInt("CoinScore").ToString();
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
            PlayerPrefs.SetInt("CoinScore", PlayerPrefs.GetInt("CoinScore") + 1);
            scoreText.text = PlayerPrefs.GetInt("CoinScore").ToString();           
            coinSource.Play();
        }
        
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
        if (endLevelPanel)
        {
            endLevelPanel.SetActive(true);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
