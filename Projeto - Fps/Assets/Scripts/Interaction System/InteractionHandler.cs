using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public delegate void InteractionStateHandler(bool state);
    public event InteractionStateHandler NotifyInteractionState;
    
    [SerializeField] private SphereCastData sphereCastData = default;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
    
    private float _rayDistance = default;
    private float _radius = default;
    private LayerMask _layerMask = default;
    
    private InteractionType _interactionType = default;
    
    private Transform _mainCamera = default;
    private Iinteractable _interactableItem = default;
    
    private bool _isInteracting = false;
    private bool _isHolded = false;
    private bool _hit = false;
    private RaycastHit _hitInfo = default;
    
    private void Awake()
    {
        InitializeSettings();
    }
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnInteractionPerformed += CheckInteractions;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnInteractionPerformed += CheckInteractions;
    }

    private void Update()
    {
        CheckRaycast();
    }

    private void InitializeSettings()
    {
        _mainCamera = Camera.main.transform;
        _rayDistance = sphereCastData.RayDistance;
        _radius = sphereCastData.RayRadius;
        _layerMask = sphereCastData.InteractableLayerMask;
    }
    
    private void CheckInteractions()
    {
        _isHolded = !_isHolded;
        
        if (_hit && _isHolded && !_isInteracting)
        {
            _hitInfo.transform.gameObject.TryGetComponent
                (out _interactableItem);

            if (_interactableItem != null)
            {
                EnableCameras(false, true);
                _isInteracting = true;
            }
        }
        
        else if (_isInteracting && !_isHolded)
        {
            EnableCameras(true,false);
            _isInteracting = false;
        }
        
        NotifyInteractionState?.Invoke(_isInteracting);
    }

    private void EnableCameras(bool activeLocalCamera, bool activeItemCamera)
    {
        _virtualCamera.gameObject.SetActive(activeLocalCamera);
        _interactableItem.OnInteractionPerformed(activeItemCamera);
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

