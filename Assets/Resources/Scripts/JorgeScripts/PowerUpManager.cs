using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject[] allBricksInGame;
    public float normalBricks;
    public GameObject powerUpPrefab;
    public float powerUpsBricks;
    public float maxPowerUpsBricks = 3;

    public Dictionary<string, float> _dic = new();

    private void Awake()
    {
        CalculateStartBricks();
        _dic["PowerUp"] = powerUpsBricks;
        _dic["Empty"] = normalBricks;
        allBricksInGame = null; // Chau Lista
    }

    public void UpdateRandom() // Actualiza valores de cantidad de powerups y cajas
    {
        _dic["Empty"] = normalBricks;
        _dic["PowerUp"] = powerUpsBricks;
    }

    public void CalculateStartBricks()
    {
        allBricksInGame = GameObject.FindGameObjectsWithTag("Brick");
        powerUpsBricks = maxPowerUpsBricks;
        normalBricks = allBricksInGame.Length - powerUpsBricks;
    }
}