using UnityEngine;

public class RectCollider : MonoBehaviour
{
    [SerializeField] private CollisionsManager _colMngr;

    [SerializeField] private LayerMask layersToCheck;

    private MeshRenderer _meshRenderer;
    private Vector3 center;
    private float height;
    private float width;

    public float xMin => center.x - width;
    public float xMax => center.x + width;
    public float yMin => center.z - height;
    public float yMax => center.z + height;

    public LayerMask LayersToCheck => layersToCheck;

    private void Awake()
    {
        SetupVariables();
    }

    private void Update()
    {
        center = transform.position;
    }

    private void OnEnable()
    {
        _colMngr.Subscribe(this);
    }

    private void OnDisable()
    {
        _colMngr.Unsubscribe(this);
    }

    private void OnDrawGizmosSelected()
    {
        SetupVariables();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(width * 2, 0, height * 2));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, width);
    }

    // Pre computation - Caching
    private void SetupVariables()
    {
        if (!_meshRenderer) _meshRenderer = GetComponent<MeshRenderer>();
        center = transform.position;
        width = _meshRenderer.bounds.extents.x;
        height = _meshRenderer.bounds.extents.z;
    }

    // Oncollision enter replacement
    public void OnRectCollision(RectCollider rectCollider)
    {
        Debug.LogWarning($"Choque con {rectCollider}");
    }
}