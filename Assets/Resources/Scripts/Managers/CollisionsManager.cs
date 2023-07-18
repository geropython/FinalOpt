using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    private readonly List<RectCollider> _objsCollider = new();

    private void Update()
    {
        CheckCollisions();
    }

    public void Subscribe(RectCollider obj)
    {
        if (!_objsCollider.Contains(obj)) _objsCollider.Add(obj);
    }

    public void Unsubscribe(RectCollider obj)
    {
        if (_objsCollider.Contains(obj)) _objsCollider.Remove(obj);
    }

    private void CheckCollisions()
    {
        for (var i = 0; i < _objsCollider.Count; i++)
        for (var j = 0; j < _objsCollider.Count; j++)
        {
            if (_objsCollider[i] == _objsCollider[j]) continue;

            if (_objsCollider[i].LayersToCheck !=
                (_objsCollider[i].LayersToCheck | (1 << _objsCollider[j].gameObject.layer)))
                continue;

            print("before checkrect");
            if (CheckRectCollision(_objsCollider[i], _objsCollider[j]))
            {
                print("Inside checkrect");
                _objsCollider[i].OnRectCollision(_objsCollider[j]);
            }
        }
    }

    private bool CheckRectCollision(RectCollider a, RectCollider b)
    {
        return a.yMin < b.yMax &&
               a.yMax > b.yMin &&
               a.xMin < b.xMax &&
               a.xMax > b.xMin;
    }
}