using System;
using Code.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private float _panSpeed;

    private Vector2 _lastPointerPosition;
    private bool _isDragging;

    private void Update()
    {
        HandleMouse();
        HandleTouch();
    }

    private void HandleMouse()
    {
        if (Mouse.current == null)
            return;

        Vector2 currentPosition = Mouse.current.position.ReadValue();

        // Mouse pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _isDragging = true;
            _lastPointerPosition = currentPosition;
        }

        // Mouse held + dragged
        if (Mouse.current.leftButton.isPressed && _isDragging)
        {
            Vector2 delta = currentPosition - _lastPointerPosition;

            PanCamera(delta);

            _lastPointerPosition = currentPosition;
        }

        // Mouse released
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _isDragging = false;
        }
    }

    private void HandleTouch()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            _isDragging = true;
            _lastPointerPosition = touch.position.ReadValue();
        }

        if (touch.press.isPressed && _isDragging)
        {
            Vector2 currentPosition = touch.position.ReadValue();
            Vector2 delta = currentPosition - _lastPointerPosition;

            PanCamera(delta);

            _lastPointerPosition = currentPosition;
        }

        if (touch.press.wasReleasedThisFrame)
        {
            _isDragging = false;
        }
    }

    private void PanCamera(Vector2 delta)
    {
        Vector3 movement = new Vector3(-delta.x, 0f, -delta.y);

        transform.Translate(movement * _panSpeed * Time.deltaTime, Space.World);
    }
}
