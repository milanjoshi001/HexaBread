using System;
using Code.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    public InputActionAsset InputAction => _inputAsset;
    
    [SerializeField] InputActionAsset _inputAsset;
    
    private void OnEnable() => _inputAsset.Enable();

    private void OnDisable() => _inputAsset.Disable();
}
