using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    //Ball variables
     [SerializeField] public GameObject player;
     [SerializeField] public bool gameStarted = false;
     [SerializeField] public Vector3 paddleToBallVector;
     [SerializeField] private float speed = 5f; 
     private Vector3 ballVelocity;

    void Start()
    {
        paddleToBallVector = transform.position - player.transform.position;
    }
    void Update()
    {
        if (!gameStarted)
        {
            transform.position = player.transform.position + paddleToBallVector;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                ballVelocity = new Vector3(0, 0, 1) * speed;
            }
        }
        else
        {
            transform.position += ballVelocity * Time.deltaTime;
        }
    }
}