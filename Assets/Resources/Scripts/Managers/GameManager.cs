using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER SINGLETON
    public static GameManager Instance = null;
    
    //BALL AND BOTTOM BOUNDARY LOGIC:
    [SerializeField] private Ball ball;
    [SerializeField] private float bottomBoundary;
   
    //PLAYER LIVES:
    [SerializeField] private int lives = 3;
   
    //BRICKS REMAINING:
    [SerializeField] private int bricksRemaining;

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
        //For not destroying the ball instance in MAIN MENU
        if (ball != null && ball.transform.position.z < bottomBoundary)
        {
            print("La pelota tocó fondo. Se pierde una vida");
            LoseLife();
        }
    
        if (bricksRemaining <= 0)
        {
            WinGame();
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
        print("Game Over!");
        //ACTIVAR PANEL,CON BOTONES RETRY Y MAIN MENU
    }
    
    public void WinGame()
    {
        //Break all the bricks
        print("You Win!!.");
        //ACTIVAR WINPANEL, CON BOTON DE MAIN MENU.
    }
}