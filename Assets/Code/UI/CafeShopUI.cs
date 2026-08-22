using Code.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CafeShopUI : Singleton<CafeShopUI>
{
    [SerializeField] private TextMeshProUGUI _totalHexagonText;
    [SerializeField] private Button _backButton;
    
    private bool _isHolding;

    private void Start()
    {
        _totalHexagonText.SetText($"{LevelHandler.Instance.TotalHexagons}");
        
        _backButton.onClick.AddListener(BackButton);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
    }

    public void Activate(bool value) => gameObject.SetActive(value);
    
    public void OnPointerDown() => _isHolding = true;

    public void OnPointerUp() => _isHolding = false;

    public void OnPointerExit() => _isHolding = false;
    
    private void Update()
    {
        if (_isHolding)
        {
            CafeShopObjectManager.Instance.ObjectFillingProcess();
            _totalHexagonText.SetText($"{LevelHandler.Instance.TotalHexagons}");
        }
    }

    private void BackButton()
    {
        LevelHandler.Instance.CloseLevel();
    }
}