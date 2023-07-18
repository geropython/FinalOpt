using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float speed = 5f;

   //UTILIZAR CUSTOM UPDATE
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontalInput, 0, 0) * (speed * Time.deltaTime);
        LimitMovePlayer();
    }
    void LimitMovePlayer() //Limita el movimiento del player (posicion hardcodeada)
    {
        if (transform.position.x < 2)
        {
            transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 24)
        {
            transform.position = new Vector3(24, transform.position.y, transform.position.z);
        }
    }
}