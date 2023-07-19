using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    /*private void OnMouseDown() // Reemplazar esto con colision con pelotita
    {
        BrickDestroy();
        CheckCollision();
    }*/

    public List<GameObject> objectsToCheck; // Lista de GameObjects que serán chequeados
    public float collisionDistance = 1.0f; // Distancia para detectar colisiones
    private PowerUpManager _powerUpManager;
    private RectCollider _rectCollider;
    private Renderer objectRenderer;
    private ObjectPool pool;

    private void Start()
    {
        _powerUpManager = FindObjectOfType<PowerUpManager>();
        pool = FindObjectOfType<ObjectPool>();

        objectRenderer = GetComponent<Renderer>();
        _rectCollider = GetComponent<RectCollider>();
        _rectCollider.OnRectCollisionEnter2D += OnCollisionHandler;
    }

    private void Update()
    {
        CheckCollision();
        for (var i = 0; i < objectsToCheck.Count; i++)
        {
            var currentObject = objectsToCheck[i];
            if (currentObject != gameObject) // Evitar comparar el objeto consigo mismo
            {
                var distance = Vector3.Distance(transform.position, currentObject.transform.position);
                if (distance <= collisionDistance)
                    // Se ha detectado una colisión con el objeto actual en la lista
                    Debug.Log("Colisión detectada con: " + currentObject.name);
            }
        }
    }

    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        var ballPosition = pool.ball.transform.position; // Me guardo la posicion del player
        var objectPosition = transform.position;
        var ballRender = pool.ball.GetComponent<Renderer>(); // Me guardo el render del player

        var distanceX = Mathf.Abs(ballPosition.x - objectPosition.x);
        var distanceZ = Mathf.Abs(ballPosition.z - objectPosition.z);

        var sumHalfWidths = ballRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        var sumHalfHeights = ballRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            BrickDestroy();
            //posZMove = 1;
        }
    }

    public void BrickDestroy()
    {
        _powerUpManager.UpdateRandom(); // Actualiza valores random
        var item = PowerUpRandom.Roulette(_powerUpManager._dic);

        if (item == "PowerUp" && _powerUpManager.powerUpsBricks > 0)
        {
            Debug.Log("POWERUP");
            _powerUpManager.powerUpsBricks -= 1;
            var multiBall = Instantiate(_powerUpManager.powerUpPrefab, transform.position, Quaternion.identity);
        }

        _powerUpManager.normalBricks -= 1;
        item = null;
        gameObject.SetActive(false);
    }

    private void OnCollisionHandler(RectCollider rectCollider)
    {
        BrickDestroy();
    }
}