using UnityEngine;

[RequireComponent(typeof(RectCollider))]
public class Ball : MonoBehaviour
{
    //Ball variables
    [SerializeField] public GameObject player;
    [SerializeField] public bool gameStarted;
    [SerializeField] public Vector3 paddleToBallVector;
    [SerializeField] private float speed = 5f;

    private RectCollider _rectCollider;
    private Vector3 ballVelocity;

    private void Awake()
    {
        _rectCollider = GetComponent<RectCollider>();
        _rectCollider.OnRectCollisionEnter2D += OnCollisionHandler;
    }

    private void Start()
    {
        paddleToBallVector = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (!gameStarted)
        {
            transform.position = player.transform.position + paddleToBallVector;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                ballVelocity = new Vector3(1, 0, 1) * speed;
            }
        }
        else
        {
            transform.position += ballVelocity * Time.deltaTime;
        }
    }

    private void OnCollisionHandler(RectCollider rectCollider)
    {
        print(ballVelocity);
        ballVelocity *= -1;
        print(ballVelocity);
    }
}