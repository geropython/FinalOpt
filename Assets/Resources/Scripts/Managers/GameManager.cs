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
        _lives--;
        if (_lives <= 0)
            GameOver();
        else
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
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        //Break all the bricks
        print("You Win!!.");
        //ACTIVAR WINPANEL, CON BOTON DE MAIN MENU.
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void ResetGameState()
    {
        gameStart = false;
        _lives = maxLives;
        PowerUpManager.CalculateStartBricks();
        _bricksToDestroy = 0 - PowerUpManager.powerUpsBricks;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
}