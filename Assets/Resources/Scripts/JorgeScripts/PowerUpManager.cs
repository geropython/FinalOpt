using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject[] allBricksInGame;
    public float normalBricks;
    public GameObject powerUpPrefab;
    public float powerUpsBricks;

    public Dictionary<string, float> _dic = new Dictionary<string, float>();
    private void Awake()
    {
        allBricksInGame = GameObject.FindGameObjectsWithTag("Brick");
        normalBricks = (allBricksInGame.Length - powerUpsBricks);
        _dic["PowerUp"] = powerUpsBricks;
        _dic["Empty"] = normalBricks;
        allBricksInGame = null; // Chau Lista
    }
    public void UpdateRandom() // Actualiza valores de cantidad de powerups y cajas
    {
        _dic["Empty"] = normalBricks;
        _dic["PowerUp"] = powerUpsBricks;
    }
}
