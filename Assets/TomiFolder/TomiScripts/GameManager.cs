using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER SINGLETON
    public static GameManager Instance = null;
    
    //BALL AND BOTTOM BOUNDARY LOGIC:
    [SerializeField] private Ball ball;
    [SerializeField] private float bottomBoundary;
   
   //PLAYER LIVES:
   [SerializeField] private int lives = 3;
   
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    //USE CUSTOM UPDATE:
    void Update()
    {
        if (ball.transform.position.z < bottomBoundary)
        {
           print("La pelota tocó fondo. Se pierde una vida");
            LoseLife();
        }
    }
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Reiniciar la posición de la pelota y volver a pegarla al player
            ball.gameStarted = false;
            ball.transform.position = ball.player.transform.position + ball.paddleToBallVector;
        }
    }
    public void GameOver()
    {
        // Player Lives = 0.
    }
    
    public void WinGame()
    {
        //Break all the bricks
    }
}