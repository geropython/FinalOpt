using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    PowerUpManager _powerUpManager;
    ObjectPool pool;
    private Renderer objectRenderer;
    public void CheckCollision() // Es la utilizada en 2D adaptado al 3D
    {
        Vector3 ballPosition = pool.ball.transform.position; // Me guardo la posicion del player
        Vector3 objectPosition = transform.position;
        Renderer ballRender = pool.ball.GetComponent<Renderer>(); // Me guardo el render del player

        float distanceX = Mathf.Abs(ballPosition.x - objectPosition.x);
        float distanceZ = Mathf.Abs(ballPosition.z - objectPosition.z);

        float sumHalfWidths = ballRender.bounds.size.x / 2 + objectRenderer.bounds.size.x / 2;
        float sumHalfHeights = ballRender.bounds.size.z / 2 + objectRenderer.bounds.size.z / 2;

        if (distanceX <= sumHalfWidths && distanceZ <= sumHalfHeights)
        {
            Debug.Log("Colision");
            BrickDestroy();
            //posZMove = 1;
        }
    }
    void Start()
    {
        _powerUpManager = FindObjectOfType<PowerUpManager>();
        pool = FindObjectOfType<ObjectPool>();

        objectRenderer = GetComponent<Renderer>();

        float visualWidth = objectRenderer.bounds.size.x;
        float visualHeight = objectRenderer.bounds.size.z;

        Debug.Log("Anchura visual: " + visualWidth);
        Debug.Log("Altura visual: " + visualHeight);
    }

    public void BrickDestroy()
    {
        _powerUpManager.UpdateRandom(); // Actualiza valores random
        var item = PowerUpRandom.Roulette(_powerUpManager._dic); 

        if (item == "PowerUp" && _powerUpManager.powerUpsBricks > 0)
        {
            Debug.Log("POWERUP");
            _powerUpManager.powerUpsBricks -= 1;
            GameObject multiBall = Instantiate(_powerUpManager.powerUpPrefab, transform.position, Quaternion.identity);
        }
        _powerUpManager.normalBricks -= 1;
        item = null;
        this.gameObject.SetActive(false);
    }

    /*private void OnMouseDown() // Reemplazar esto con colision con pelotita
    {
        BrickDestroy();
        CheckCollision();
    }*/

    public List<GameObject> objectsToCheck; // Lista de GameObjects que serán chequeados
    public float collisionDistance = 1.0f; // Distancia para detectar colisiones

    private void Update()
    {
        CheckCollision();
        for (int i = 0; i < objectsToCheck.Count; i++)
        {
            GameObject currentObject = objectsToCheck[i];
            if (currentObject != gameObject) // Evitar comparar el objeto consigo mismo
            {
                float distance = Vector3.Distance(transform.position, currentObject.transform.position);
                if (distance <= collisionDistance)
                {
                    // Se ha detectado una colisión con el objeto actual en la lista
                    Debug.Log("Colisión detectada con: " + currentObject.name);
                }
            }
        }
    }
}
