using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    //public GameObject brick1;
    //public Renderer player;
    public float extraBallSpeed = 5f;
    ObjectPool pool;
    float posXMove = 1;
    float posZMove = 1;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        Vector3 playerPosition = pool.player.transform.position; // Me guardo la posicion del player
        Vector3 objectPosition = transform.position;
        Renderer playerRender = pool.player.GetComponent<Renderer>(); // Me guardo el render del player

        float distanceX = Mathf.Abs(playerPosition.x - objectPosition.x);
        float distanceZ = Mathf.Abs(playerPosition.z - objectPosition.z);

        float sumHalfWidths = playerRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        float sumHalfHeights = playerRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            posZMove = 1;
        }
    }
    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }
    private void OnEnable()
    {
        transform.position = pool.player.transform.position;
    }

    private void Update()
    {
        transform.position += new Vector3(posXMove, 0, posZMove) * extraBallSpeed * Time.deltaTime;
        MoveExtraBall();
        CheckCollision();
    }
    public void SetDir()
    {
        posXMove = -posXMove;
    }
    void MoveExtraBall()
    {
        if (transform.position.z > 26)
        {
            posZMove = -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, 25.5f);
        }
        if (transform.position.x < 0)
        {
            posXMove = 1;
            transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 26)
        {
            posXMove = -1;
            transform.position = new Vector3(25.5f, transform.position.y, transform.position.z);
        }
    }
}
