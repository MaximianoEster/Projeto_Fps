using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public delegate void LocomotionInputsHandler(InputsData currentInputsData);
    public event LocomotionInputsHandler OnAnyKeyPressed;

    public delegate void ActionsInputsHandler();
    public ActionsInputsHandler OnTabPerformed;
    public ActionsInputsHandler OnAttackPerformed;
    public ActionsInputsHandler OnAimPerformed;
    public ActionsInputsHandler OnInteractionPerformed;
    
    private PlayerControls _playerControls = default;
    private InputsData _currentInputsData = default;

    private Vector2 _keyboardDirection = default;
    private Vector2 _mouseDirection = default;
    private bool _isJumping = false;
    
    private InteractionType _interactionType = InteractionType.NONE;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        SubscribeContextOnPerformed();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        OnAnyKeyPressed?.Invoke(CreateStruct());
        ResetStruct();
    }

    private void SubscribeContextOnPerformed()
    {
        _playerControls.Player.Locomotion.performed += GetKeyboardInput;
        _playerControls.Player.Look.performed += GetMouseDeltaInput;
        _playerControls.Player.Jump.performed += GetJumpInput;
        
        _playerControls.Player.Interaction.performed += GetInteractionInput;
        _playerControls.Player.Attacking.performed += GetAttackInput;
        
        _playerControls.Player.Aim.performed += AimPerformed;
        _playerControls.Player.Aim.canceled += AimCanceled;
        
        _playerControls.Player.WheelWeapon.performed += TabPerformed;
        _playerControls.Player.WheelWeapon.canceled += TabCanceled;
    }
    
    private void GetKeyboardInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _keyboardDirection = _playerControls.Player.Locomotion.ReadValue<Vector2>();
        }
    }
    
    private void GetMouseDeltaInput(InputAction.CallbackContext ctx)
    {
        _mouseDirection = _playerControls.Player.Look.ReadValue<Vector2>();
    }

    private void GetAttackInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnAttackPerformed?.Invoke();
        }
    }
    
    private void GetJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isJumping = true;
        }
    }
    
    private void GetInteractionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is HoldInteraction)
        {
            OnInteractionPerformed?.Invoke();
        }
        else if(ctx.interaction is PressInteraction)
        {
            OnInteractionPerformed?.Invoke();
        }
    }

    private void TabPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnTabPerformed?.Invoke();
        }
    }

    private void TabCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            OnTabPerformed?.Invoke();
        }
    }

    private void AimPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnAimPerformed?.Invoke();
        }
    }

    private void AimCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            OnAimPerformed?.Invoke();
        }
    }

    private InputsData CreateStruct()
    {
        return _currentInputsData = new InputsData(_keyboardDirection, _mouseDirection, _isJumping);
    }

    private void ResetStruct()
    {
        _isJumping = false;
        _mouseDirection = Vector3.zero;
        _interactionType = InteractionType.NONE;
    }
}
