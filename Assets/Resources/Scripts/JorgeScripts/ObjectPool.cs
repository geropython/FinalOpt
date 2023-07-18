using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] List<GameObject> ballList;
    [SerializeField] int poolSizeBall = 10;
    public GameObject player; //REFERENCIA PARA EL INSTANCIAMIENTO EN OTRO SCRIPT

    public int dirBall = 1;

    private void Start()
    {
        AddBallsToPool(poolSizeBall);
    }
    private void AddBallsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ballList.Add(ball);
            ball.transform.parent = transform; // Instancia como hijos de este gameobject
        }
    }
    public GameObject RequestBall()
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            if (!ballList[i].activeSelf)
            {
                ballList[i].SetActive(true);
                return ballList[i];
            }
        }
        return null;
    }
}
