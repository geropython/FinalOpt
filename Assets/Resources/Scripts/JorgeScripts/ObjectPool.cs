using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private List<GameObject> ballList;
    [SerializeField] private int poolSizeBall = 10;
    public GameObject player; //REFERENCIA PARA EL INSTANCIAMIENTO EN OTRO SCRIPT

    public Renderer ball; // AGREGARDO

    private void Start()
    {
        AddBallsToPool(poolSizeBall);
    }

    private void AddBallsToPool(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ballList.Add(ball);
            ball.transform.parent = transform; // Instancia como hijos de este gameobject
        }
    }

    public GameObject RequestBall()
    {
        for (var i = 0; i < ballList.Count; i++)
            if (!ballList[i].activeSelf)
            {
                ballList[i].SetActive(true);
                return ballList[i];
            }

        return null;
    }
}