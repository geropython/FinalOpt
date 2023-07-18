using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    PowerUpManager _powerUpManager;
    ObjectPool pool;
    void Start()
    {
        _powerUpManager = FindObjectOfType<PowerUpManager>();
        pool = FindObjectOfType<ObjectPool>();
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

    private void OnMouseDown() // Reemplazar esto con colision con pelotita
    {
        BrickDestroy();
    }
}
