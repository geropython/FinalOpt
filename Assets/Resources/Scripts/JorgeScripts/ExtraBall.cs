using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    [SerializeField] private float bottomBoundary;

    //public GameObject brick1;
    //public Renderer player;
    public float extraBallSpeed = 5f;
    private Vector3 _ballDirection;
    private RectCollider _rectCollider;
    private Renderer objectRenderer;
    private ObjectPool pool;
    private float posXMove = 1;
    private float posZMove = 1;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        _rectCollider = GetComponent<RectCollider>();
        _rectCollider.OnRectCollisionEnter2D += OnCollisionHandler;
        _ballDirection = new Vector3(posXMove, 0, posZMove);
    }

    private void Update()
    {
        //_ballDirection = new Vector3(posXMove, 0, posZMove);
        transform.position += _ballDirection * (extraBallSpeed * Time.deltaTime);
        OutOfBounds();
        CheckCollision();
    }

    private void OnEnable()
    {
        var startPosition = new Vector3(pool.player.transform.position.x, 0, pool.player.transform.position.z + 0.5f);
        transform.position = startPosition;
    }

    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        var playerPosition = pool.player.transform.position; // Me guardo la posicion del player
        var objectPosition = transform.position;
        var playerRender = pool.player.GetComponent<Renderer>(); // Me guardo el render del player

        var distanceX = Mathf.Abs(playerPosition.x - objectPosition.x);
        var distanceZ = Mathf.Abs(playerPosition.z - objectPosition.z);

        var sumHalfWidths = playerRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        var sumHalfHeights = playerRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            posZMove = 1;
            _ballDirection.z = posZMove;
        }
    }

    public void SetDir()
    {
        posXMove = -posXMove;
        _ballDirection.x = posXMove;
    }

    private void OutOfBounds()
    {
        if (transform.position.z < bottomBoundary)
        {
            gameObject.SetActive(false);
            GameManager.Instance.LoseLife();
        }

        if (transform.position.z > 26)
        {
            posZMove = -1;
            _ballDirection.z = posZMove;
            transform.position = new Vector3(transform.position.x, transform.position.y, 25.5f);
        }

        if (transform.position.x < 0)
        {
            posXMove = 1;
            _ballDirection.x = posXMove;
            transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 26)
        {
            posXMove = -1;
            _ballDirection.x = posXMove;
            transform.position = new Vector3(25.5f, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionHandler(RectCollider rectCollider)
    {
        print(_ballDirection);
        _ballDirection *= -1;
        print(_ballDirection);
    }
}