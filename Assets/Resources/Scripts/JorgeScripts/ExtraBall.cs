using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    public float extraBallSpeed = 5f;
    ObjectPool pool;
    float posXMove = 1;
    float posZMove = 1;


    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>();
    }
    private void OnEnable()
    {
        transform.position = pool.player.transform.position;
    }
    public void CheckCollision()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position += new Vector3(posXMove, 0, posZMove) * extraBallSpeed * Time.deltaTime;
    }
    public void GetDir()
    {
        posXMove = -posXMove;
    }
}
