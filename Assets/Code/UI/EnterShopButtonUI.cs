using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnterShopButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private Image _levelImage;
    private  Button _shopButton;

    public void SetLevelData(string levelName, Sprite  levelSprite, UnityAction callback)
    {
        _levelNameText.SetText(levelName);
        _levelImage.sprite = levelSprite;
        _shopButton.onClick.AddListener(() => callback?.Invoke());
    }
    
}