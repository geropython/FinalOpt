using System.Collections.Generic;
using UnityEngine;

// Las justificaciones o aplicaciones de patrones estan comentadas en el script CustomUpdateManager
// Este script es una copia con algunas variables diferentes 
public class CustomUpdateManagerUI : MonoBehaviour
{
    private List<ManagedUpdateBehaviourUI> _list;
    private const float FRAME_TIME_30 = 0.03333f;
    private const float FRAME_TARGET = 30f;
    private float _tempTime;

    private void Awake()
    {
        _list = new List<ManagedUpdateBehaviourUI>(FindObjectsOfType<ManagedUpdateBehaviourUI>());
    }

    private void Update()
    {
        UpdateVersion1();
    }

    private void UpdateVersion1()
    {
        _tempTime += Time.deltaTime;
        if (!(_tempTime >= FRAME_TIME_30)) return;
        var count = _list.Count;
        for (var i = 0; i < count; i++) _list[i].UpdateMe();

        _tempTime = 0;
    }

    public void AddToList(ManagedUpdateBehaviourUI managed)
    {
        if (!_list.Contains(managed)) _list.Add(managed);
    }

    public void RemoveFromList(ManagedUpdateBehaviourUI managed)
    {
        if (_list.Contains(managed)) _list.Remove(managed);
    }

    public void ClearList()
    {
        _list.Clear();
    }
}