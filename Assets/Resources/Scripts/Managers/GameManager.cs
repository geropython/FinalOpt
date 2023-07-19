using TMPro;
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
    [SerializeField] private TextMeshProUGUI livesAmount;
    private int _ballsOnScreen;
    private float _bricksToDestroy;
    private bool _gameComplete;

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
        Time.timeScale = 1f;
        ResetGameState();
    }

    //USE CUSTOM UPDATE:
    private void Update()
    {
        if (PowerUpManager.normalBricks <= _bricksToDestroy && !_gameComplete) WinGame();
    }

    public void LoseLife()
    {
        _lives--;
        livesAmount.text = _lives.ToString();
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
        //ACTIVAR PANEL,CON BOTONES RETRY Y MAIN MENU
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        //Break all the bricks
        _gameComplete = true;
        //ACTIVAR WINPANEL, CON BOTON DE MAIN MENU.
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void ResetGameState()
    {
        gameStart = false;
        _lives = maxLives;
        livesAmount.text = _lives.ToString();
        PowerUpManager.CalculateStartBricks();
        _bricksToDestroy = 0 - PowerUpManager.powerUpsBricks;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        _gameComplete = false;
    }
}