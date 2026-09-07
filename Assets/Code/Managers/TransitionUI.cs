using System;
using System.Collections;
using Code.Utils;
using UnityEngine;
using UnityEngine.UI;

public class TransitionUI : Singleton<TransitionUI>
{
    [Header("Elements")] 
    [SerializeField] private Transform _topPanel;
    [SerializeField] private Transform _bottomPanel;

    private Coroutine _transitionCoroutine;
    private Image _blockerImage;
    
    private void Start()
    {
        TryGetComponent(out _blockerImage);
    }

    public void StartTransition()
    {
        _blockerImage.enabled = true;
        if(_transitionCoroutine != null)
            StopTransition();
        
        _transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        LeanTween.scaleY(_topPanel.gameObject, 1f, 1f);
        LeanTween.scaleY(_bottomPanel.gameObject, 1f, 1f);
        yield return new WaitForSeconds(5f);
        StopTransition();
    }

    private void StopTransition()
    {
        LeanTween.scaleY(_topPanel.gameObject, 0f, 1f).setOnComplete(() => _blockerImage.enabled = false);
        LeanTween.scaleY(_bottomPanel.gameObject, 0f, 1f).setOnComplete(() => _blockerImage.enabled = false);
        
        _transitionCoroutine = null;
    }
}