using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Renderer _renderer;

    [SerializeField] private Collider _hexagonCollider;
    public HexagonStack HexStack { get; private set; }

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }

    public void Configure(HexagonStack hexStack)
    {
        HexStack = hexStack;
    }

    public void DisableCollider()
    {
        _hexagonCollider.enabled = false;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
