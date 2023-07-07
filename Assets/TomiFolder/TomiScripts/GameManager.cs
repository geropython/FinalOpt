using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int gameScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void WinGame()
    {
        // WinGame
    }

    public void LoseGame()
    {
        // GAme over
    }

    public void GameScore()
    {
        // GameScore Update
    }
}