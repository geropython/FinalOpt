using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public bool gameStart;
    private ObjectPool pool;

    private void Start()
    {
        gameStart = false;
        pool = FindObjectOfType<ObjectPool>();
        GameManager.Instance.ResetGameState();
    }

    //UTILIZAR CUSTOM UPDATE
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontalInput, 0, 0) * (speed * Time.deltaTime);
        LimitMovePlayer();
        StartGame();
    }

    private void LimitMovePlayer() //Limita el movimiento del player (posicion hardcodeada)
    {
        if (transform.position.x < 2) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        if (transform.position.x > 24) transform.position = new Vector3(24, transform.position.y, transform.position.z);
    }

    private void StartGame()
    {
        if (!GameManager.Instance.gameStart)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.gameStart = true;
                pool.RequestBall();
            }
    }
}