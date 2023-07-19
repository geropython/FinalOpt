using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : MonoBehaviour
{
    ObjectPool pool;
    PowerUpManager _powerUpManager;
    [SerializeField] float speed;
    public bool playerCollision;
    Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        _powerUpManager = FindObjectOfType<PowerUpManager>();
        pool = FindObjectOfType<ObjectPool>();
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        CheckCollision();
    }
    private void CreateBalls() //Instancia dos pelotitas en direcciones opuestas (eje X)
    {
        GameObject ball1 = pool.RequestBall();
        GameObject ball2 = pool.RequestBall();
        if (ball1 != null && ball2 != null)
        {
            ball2.GetComponent<ExtraBall>();
            ball1.GetComponent<ExtraBall>().SetDir();
        }
    }

    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        Vector3 playerPosition = pool.player.transform.position; // Me guardo la posicion del player
        Vector3 objectPosition = transform.position;
        //Renderer playerRender = pool.player.GetComponent<Renderer>(); // Me guardo el render del player
        Renderer playerRender = pool.player.GetComponent<Renderer>();

        float distanceX = Mathf.Abs(playerPosition.x - objectPosition.x);
        float distanceZ = Mathf.Abs(playerPosition.z - objectPosition.z);

        float sumHalfWidths = playerRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        float sumHalfHeights = playerRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            gameObject.SetActive(false);
            CreateBalls();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CreateBalls();
            this.gameObject.SetActive(false);
        }
    }*/
}
