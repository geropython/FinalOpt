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
    }
    
}