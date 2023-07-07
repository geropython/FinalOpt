using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    private static List<ManagedUpdateBehaviour> _list;

    //No es hashing debido a que no constituye una tabla de valores.
    private const float FRAME_TIME_60 = 0.01666f;
    private const int FRAME_TARGET = 60;
    private float _tempTime;

    private void Awake()
    {
        // Precomputation porque busca todos los objetos al comienzo de la escena
        // Esto se podria hacer cada vez que trata de hacer un update pero no seria optimo
        _list = new List<ManagedUpdateBehaviour>(FindObjectsOfType<ManagedUpdateBehaviour>());
    }

    private void Update()
    {
        UpdateVersion1();
    }

    private void UpdateVersion1()
    {
        _tempTime += Time.deltaTime;
        // Lazy computation porque retrasa toda la parte que sigue hasta que es necesaria porque se cumple la condicion¿?¿?
        // No me parece correcto porque sino todos los IF son lazy computation
        if (!(_tempTime >= FRAME_TIME_60)) return;
        // Caching para luego utilizar este valor en el for 
        // Este valor se podria poner directo en el for, pero cada iteracion lo tendria que volver a calcular
        var count = _list.Count;
        for (var i = 0; i < count; i++) _list[i].UpdateMe();

        _tempTime = 0;
    }

    public void AddToList(ManagedUpdateBehaviour managed)
    {
        if (!_list.Contains(managed)) _list.Add(managed);
    }

    public void RemoveFromList(ManagedUpdateBehaviour managed)
    {
        if (_list.Contains(managed)) _list.Remove(managed);
    }

    public static void ClearList()
    {
        _list.Clear();
    }
}