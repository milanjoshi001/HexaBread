using System;
using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeShopUI : Singleton<CafeShopUI>, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _totalHexagonText;

    private void Start()
    {
        _totalHexagonText.SetText($"{LevelHandler.Instance.TotalHexagons}");
    }

    public void Activate(bool value) => gameObject.SetActive(value);
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Object filling process started!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Object filling process stopped!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Object filling process stopped!");
    }
}