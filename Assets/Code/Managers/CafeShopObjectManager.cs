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
    }

    private void OnEnable()
    {
        CheckObjectFill();
    }

    public void CheckObjectFill()
    {
        var objectsFill = _objectFills.Where(item => item.gameObject.activeSelf);

        foreach (var objectFill in objectsFill)
        {
            if (objectFill.FillPercentage <= objectFill.MaxFillPercentage)
            {
                _objectFill = objectFill;
            }
            else
                LoadObject();
        }
    }

    public void ObjectFillingProcess()
    {
        if (_objectFill != null && _objectFill.FillPercentage <= _objectFill.MaxFillPercentage)
            _objectFill.Fill();
    }

    private void LoadObject()
    {
        foreach (var objectFill in _objectFills)
        {
            if (!objectFill.gameObject.activeSelf)
            {
                objectFill.gameObject.SetActive(true);
                break;
            }
        }
    }
}