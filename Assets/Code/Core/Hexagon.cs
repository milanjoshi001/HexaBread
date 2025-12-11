using System.Collections;
using TMPro;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _numOfHexagonsTopText;
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
    
    public void ActivateCollider(bool value)
    {
        _hexagonCollider.enabled = value;
    }

    public void SetSimilarHexagonCount(int value)
    {
        _numOfHexagonsTopText.SetText($"{value}");
    }
    
    public void EnableText(bool value) => _numOfHexagonsTopText.gameObject.SetActive(value);

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void MoveToLocal(Vector3 targetLocalPos)
    {
        LeanTween.cancel(gameObject);

        float delay = transform.GetSiblingIndex() * 0.01f;

        LeanTween.moveLocal(gameObject, targetLocalPos, 0.1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(transform.GetSiblingIndex() * 0.01f);
        
        Vector3 direction = (targetLocalPos - transform.localPosition).With(y: 0).normalized;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        LeanTween.rotateAround(gameObject, rotationAxis, 360, 0.05f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(delay);
    }

    public void Vanish(float delay)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.zero, 0.2f)
            .setEase(LeanTweenType.easeInBack)
            .setDelay(delay)
            .setOnComplete(() => Destroy(gameObject));
    }
}
