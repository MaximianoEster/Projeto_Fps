using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public delegate void InputsHandler(InputsData currentInputsData);
    public event InputsHandler OnAnyKeyPressed;

    private PlayerControls _playerControls = default;
    private InputsData _currentInputsData = default;

    private Vector2 _keyboardDirection = default;
    private Vector2 _mouseDirection = default;

    private bool _isAttacking = false;
    private bool _isJumping = false;
    private bool _isAiming = false;

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
        _playerControls.Player.Aim.performed += GetAimInput;
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
            _isAttacking = true;
        }
    }
    
    private void GetJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isJumping = true;
        }
    }

    private void GetAimInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isAiming = true;
        }
    }

    private void GetInteractionInput(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is PressInteraction)
        {
            _interactionType = InteractionType.PRESSED;
        }
        else if(ctx.interaction is HoldInteraction)
        {
            _interactionType = InteractionType.HOLDED;
        }
    }

    private InputsData CreateStruct()
    {
        return _currentInputsData = new InputsData(_keyboardDirection, _mouseDirection,
            _isAttacking, _isJumping, _isAiming, _interactionType);
    }

    private void ResetStruct()
    {
        _isAttacking = false;
        _isJumping = false;
        _mouseDirection = Vector3.zero;
        _interactionType = InteractionType.NONE;
    }
}
