using System;
using System.Collections;
using Code.Utils;
using UnityEngine;
using UnityEngine.Events;
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

    public void StartTransition(UnityAction callback)
    {
        _blockerImage.enabled = true;
        if(_transitionCoroutine != null)
            StopTransition();
        
        _transitionCoroutine = StartCoroutine(TransitionCoroutine(callback));
    }

    private IEnumerator TransitionCoroutine(UnityAction callback)
    {
        LeanTween.scaleY(_topPanel.gameObject, 1f, 0.2f);
        LeanTween.scaleY(_bottomPanel.gameObject, 1f, 0.2f);
        yield return new WaitForSeconds(1f);
        callback?.Invoke();
        StopTransition();
    }

    private void StopTransition()
    {
        LeanTween.scaleY(_topPanel.gameObject, 0f, 0.2f).setOnComplete(() => _blockerImage.enabled = false);
        LeanTween.scaleY(_bottomPanel.gameObject, 0f, 0.2f).setOnComplete(() => _blockerImage.enabled = false);
        
        _transitionCoroutine = null;
    }
}