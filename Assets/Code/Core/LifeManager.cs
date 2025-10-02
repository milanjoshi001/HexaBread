using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    
    [SerializeField] TextMeshProUGUI _lifeText;
    [SerializeField] TextMeshProUGUI _remainingTimeText;
    
    [SerializeField] private int _totalLife;
    [SerializeField] private float _regenerationTime;

    private float _remainingTime = 0;
    
    private int _lifeLeft;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _lifeLeft = _totalLife;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _lifeText.SetText(_lifeLeft.ToString());
        _remainingTimeText.SetText("");
    }

    private void Update()
    {
        if (_lifeLeft < _totalLife)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                
                int minutes = Mathf.FloorToInt(_remainingTime / 60);
                int seconds = Mathf.FloorToInt(_remainingTime % 60);
            
                _remainingTimeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
            }
            else
            {
                AddLife();
            }
        }
        else
        {
            _remainingTimeText.SetText("");
        }
    }

    private void AddLife()
    {
        _lifeLeft++;
        UpdateUI();

        if (_lifeLeft < _totalLife)
        {
            _remainingTime = _regenerationTime;
        }
        else
        {
            _remainingTime = 0;
        }
    }

    public void LifeGone()
    {
        _lifeLeft = Mathf.Max(0, _lifeLeft - 1);
        UpdateUI();

        if (_lifeLeft < _totalLife && _remainingTime <= 0)
            _remainingTime = _regenerationTime;
    }
}
