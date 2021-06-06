using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private SphereCastData sphereCastData = default;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
    
    private float _rayDistance = default;
    private float _radius = default;
    private LayerMask _layerMask = default;
    
    private InteractionType _interactionType = default;
    
    private Transform _mainCamera = default;
    private Iinteractable _interactableItem = default;
    
    private bool _isInteracting = false;
    private bool _hit = false;
    private RaycastHit _hitInfo = default;
    
    private void Awake()
    {
        InitializeSettings();
    }
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += CheckInteractions;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= CheckInteractions;
    }
    
    private void InitializeSettings()
    {
        _mainCamera = Camera.main.transform;
        _rayDistance = sphereCastData.RayDistance;
        _radius = sphereCastData.RayRadius;
        _layerMask = sphereCastData.InteractableLayerMask;
    }
    
    private void CheckInteractions(InputsData inputsData)
    {
        CheckRaycast();
        _interactionType = inputsData.InteractionType;
        
        if (_hit && _interactionType == InteractionType.HOLDED && !_isInteracting)
        {
            _hitInfo.transform.gameObject.TryGetComponent
                (out _interactableItem);

            if (_interactableItem != null)
            {
                EnableCameras(false, true, true);
            }
        }
        
        else if (_isInteracting && _interactionType == InteractionType.PRESSED)
        {
            EnableCameras(true,false,false);
        }
    }

    private void EnableCameras(bool activeLocalCamera, bool activeItemCamera, bool isInteracting)
    {
        _virtualCamera.gameObject.SetActive(activeLocalCamera);
        _interactableItem.Action(activeItemCamera);
        _isInteracting = isInteracting;
    }

    private void CheckRaycast()
    {
        Ray ray = new Ray(_mainCamera.position, 
            _mainCamera.transform.forward);

        _hitInfo = new RaycastHit();
        
        _hit = Physics.SphereCast(ray,_radius,
            out _hitInfo, _rayDistance, _layerMask);
        
        //Remove latter
        if (_hit)
        {
            Debug.DrawRay(_mainCamera.position,
                _mainCamera.TransformDirection(Vector3.forward) * _rayDistance, Color.blue);
        }
        else
        {
            Debug.DrawRay(_mainCamera.position,
                _mainCamera.TransformDirection(Vector3.forward) * _rayDistance, Color.red);
        }
    }
    
    public bool IsInteracting => _isInteracting;
}

