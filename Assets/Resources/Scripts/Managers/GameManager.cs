using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER SINGLETON
    public static GameManager Instance;

    //JORGE
    public bool gameStart;

    //BALL AND BOTTOM BOUNDARY LOGIC:
    [SerializeField] private Ball ball;
    [SerializeField] private float bottomBoundary;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    private int _ballsOnScreen;
    private float _bricksToDestroy;

    //PLAYER LIVES:
    private int _lives;
    public CollisionsManager CollisionsManager { get; private set; }
    public PowerUpManager PowerUpManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        CollisionsManager = GetComponent<CollisionsManager>();
        PowerUpManager = GetComponent<PowerUpManager>();
        ResetGameState();
    }

    //USE CUSTOM UPDATE:
    private void Update()
    {
        if (PowerUpManager.normalBricks <= _bricksToDestroy) WinGame();
    }

    public void LoseLife()
    {
        print("La pelota tocó fondo. Se pierde una vida");
        if (!gameStart) _lives--;
        if (_lives <= 0)
            GameOver();
        else
            // Reiniciar la posición de la pelota y volver a pegarla al player
            // ball.gameStarted = false;
            // ball.transform.position = ball.player.transform.position + ball.paddleToBallVector;
            gameStart = false;
    }

    public void SpawnNewBall()
    {
        _ballsOnScreen++;
    }

    public void LoseBall()
    {
        _ballsOnScreen--;
        if (_ballsOnScreen <= 0) LoseLife();
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

    public void ResetGameState()
    {
        gameStart = false;
        _lives = maxLives;
        PowerUpManager.CalculateStartBricks();
        _bricksToDestroy = 0 - PowerUpManager.powerUpsBricks;
    }
}