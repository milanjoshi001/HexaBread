using System;
using Code.Utils;
using UnityEngine;

public class AIStartPoint : Singleton<AIStartPoint>
{
    
    [SerializeField] CustomerAI _customerAI;
    
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;
    
    public Transform EntryPoint => _entryPoint;
    public Transform ExitPoint => _exitPoint;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_customerAI.gameObject, EntryPoint);
        }
    }
}