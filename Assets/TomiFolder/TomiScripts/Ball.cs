using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
   [SerializeField] private GameObject player;
   [SerializeField] private bool gameStarted = false;
   [SerializeField] private Vector3 paddleToBallVector;

    void Start()
    {
        paddleToBallVector = transform.position - player.transform.position;
    }

    //UTILIZAR CUSTOM UPDATE
    void Update()
    {
        if (!gameStarted)
        {
            transform.position = player.transform.position + paddleToBallVector;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                // Aca se puede aplicar una fuerza custom para tirar la pelota.
            }
        }
    }
    
    
}