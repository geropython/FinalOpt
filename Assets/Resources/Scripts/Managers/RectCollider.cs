using System;
using UnityEngine;

public class RectCollider : MonoBehaviour
{
    [SerializeField] private LayerMask layersToCheck;
    private Vector3 _center;
    private CollisionsManager _colManager;
    private float _height;

    private MeshRenderer _meshRenderer;
    private float _width;

    public float XMin => _center.x - _width;
    public float XMax => _center.x + _width;
    public float YMin => _center.z - _height;
    public float YMax => _center.z + _height;

    public LayerMask LayersToCheck => layersToCheck;

    private void Awake()
    {
        SetupVariables();
    }

    private void Update()
    {
        _center = transform.position;
    }

    private void OnEnable()
    {
        GameManager.Instance.CollisionsManager.Subscribe(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.CollisionsManager.Unsubscribe(this);
    }

    private void OnDrawGizmosSelected()
    {
        SetupVariables();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_center, new Vector3(_width * 2, 0, _height * 2));
    }

    public event Action<RectCollider> OnRectCollisionEnter2D;

    // Pre computation - Caching
    private void SetupVariables()
    {
        if (!_meshRenderer) _meshRenderer = GetComponent<MeshRenderer>();
        _center = transform.position;
        var bounds = _meshRenderer.bounds.extents;
        _width = bounds.x;
        _height = bounds.z;
    }

    // On collision enter replacement
    public void OnRectCollision(RectCollider rectCollider)
    {
        OnRectCollisionEnter2D?.Invoke(rectCollider);
    }
}