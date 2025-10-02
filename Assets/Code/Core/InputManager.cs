using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    public InputActionAsset InputAction => _inputAsset;
    
    [SerializeField] InputActionAsset _inputAsset;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    private void OnEnable() => _inputAsset.Enable();

    private void OnDisable() => _inputAsset.Disable();
}
