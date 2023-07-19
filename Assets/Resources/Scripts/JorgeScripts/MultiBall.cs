using UnityEngine;

public class MultiBall : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool playerCollision;
    private PowerUpManager _powerUpManager;
    private RectCollider _rectCollider;
    private Renderer objectRenderer;
    private ObjectPool pool;

    private void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        _powerUpManager = FindObjectOfType<PowerUpManager>();
        pool = FindObjectOfType<ObjectPool>();
        _rectCollider = GetComponentInChildren<RectCollider>();
        _rectCollider.OnRectCollisionEnter2D += OnCollisionHandler;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z - speed * Time.deltaTime);
        //CheckCollision();
    }

    private void CreateBalls() //Instancia dos pelotitas en direcciones opuestas (eje X)
    {
        var ball1 = pool.RequestBall();
        var ball2 = pool.RequestBall();
        if (ball1 != null && ball2 != null)
        {
            ball2.GetComponent<ExtraBall>();
            ball1.GetComponent<ExtraBall>().SetDir();
        }
    }

    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        var playerPosition = pool.player.transform.position; // Me guardo la posicion del player
        var objectPosition = transform.position;
        var playerRender = pool.player.GetComponent<Renderer>();

        var distanceX = Mathf.Abs(playerPosition.x - objectPosition.x);
        var distanceZ = Mathf.Abs(playerPosition.z - objectPosition.z);

        var sumHalfWidths = playerRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        var sumHalfHeights = playerRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            gameObject.SetActive(false);
            CreateBalls();
        }
    }

    private void OnCollisionHandler(RectCollider rectCollider)
    {
        gameObject.SetActive(false);
        CreateBalls();
    }
}