using System;
using Code.Utils;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : Singleton<CameraManager>
{
    [Header("Cafe Shop Settings")] 
    [SerializeField] private Vector3 _cafeShopPosition;
    [SerializeField] private Vector3 _cafeShopRotation;
    
    private Camera _camera;
    private Vector3 _cameraPosition;
    private Quaternion _cameraRotation;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraPosition = _camera.transform.position;
        _cameraRotation = _camera.transform.rotation;
    }

    public void ToggleProjection()
    {
        _camera.orthographic = !_camera.orthographic;

        if (!_camera.orthographic)
        {
            _camera.transform.position = _cafeShopPosition;
            _camera.transform.rotation = Quaternion.Euler(_cafeShopRotation);
        }
        else
        {
            _camera.transform.position = _cameraPosition;
            _camera.transform.rotation = _cameraRotation;
        }
    }
}
