using System;
using System.Collections.Generic;
using System.Linq;
using Code.Utils;
using UnityEngine;

public class CafeShopObjectManager : Singleton<CafeShopObjectManager>
{
    [SerializeField] private List<ObjectFill> _objectFills;

    private ObjectFill _objectFill;

    private void Start()
    {
        foreach (var objectFill in _objectFills)
        {
            objectFill.gameObject.SetActive(false);
        }
        CheckObjectFill();
    }

    public void Activate(bool value) => gameObject.SetActive(value);

    public void CheckObjectFill()
    {
        _objectFill = null;

        foreach (var objectFill in _objectFills)
        {
            if (objectFill.gameObject.activeSelf && !objectFill.IsFull)
            {
                _objectFill = objectFill;
                return;
            }
        }

        LoadObject();
    }

    public void ObjectFillingProcess()
    {
        if (_objectFill != null)
            _objectFill.Fill();
    }

    private void LoadObject()
    {
        foreach (var objectFill in _objectFills)
        {
            if (!objectFill.gameObject.activeSelf)
            {
                objectFill.gameObject.SetActive(true);
                _objectFill = objectFill;
                return;
            }
        }
        
        Debug.Log("Level Complete");
    }
}