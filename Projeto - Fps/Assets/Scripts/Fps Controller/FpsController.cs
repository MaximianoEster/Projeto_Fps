using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = default;
    [SerializeField] private FpsData _fpsData = default;
    [SerializeField] private InteractionHandler _interactionHandler = default;
    [SerializeField] private RotationData rotationData = default;
    
    private float _regularSpeed = default;
    private float _moveBackardsSpeed = default;
    private float _moveSideSpeed = default;
    private bool _isGrounded = default;
    
    private float _jumpHeight = default;
    private float _gravity = default;

    private Vector3 _startingRotation = Vector3.zero;
    private Vector3 _velocity = default;

    private float _minHorizontalAngleRotation = default;
    private float _maxHorizontalAngleRotation = default;
    private float _rotationSpeed = default;

    // Maybe remove latter
    private float _currentSpeed = default;

    private Vector2 _turn = default;
    private void Awake()
    {
        InitializeSettings();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += Movement;
        GameManager.Instance.InputManager.OnAnyKeyPressed += Jump;
        GameManager.Instance.InputManager.OnAnyKeyPressed += Rotation;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= Movement;
        GameManager.Instance.InputManager.OnAnyKeyPressed -= Jump;
        GameManager.Instance.InputManager.OnAnyKeyPressed -= Rotation;
    }

    private void InitializeSettings()
    {
        _regularSpeed = _fpsData.RegularSpeed;
        _moveBackardsSpeed = _fpsData.MoveBackardsSpeed;
        _moveSideSpeed = _fpsData.MoveSideSpeed;

        _jumpHeight = _fpsData.JumpHeight;
        _gravity = _fpsData.Gravity;
        
        _rotationSpeed = rotationData.HorizontalSpeed;
        _minHorizontalAngleRotation = rotationData.HorizontalAngleMin;
        _maxHorizontalAngleRotation = rotationData.HorizontalAngleMax;
    }

    private void Movement(InputsData inputsData)
    {
        if (!_interactionHandler.IsInteracting)
        {
            _isGrounded = _characterController.isGrounded;
            
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0;
            }
            
            Vector2 currentDirection = inputsData.KeyboardDirection;
            Vector3 currentMovement = new Vector3(currentDirection.x, 0f, currentDirection.y);
            
            _currentSpeed = Math.Abs(currentDirection.x) > 0 ? _moveSideSpeed :
                currentDirection.y == -1 ? _moveBackardsSpeed : _regularSpeed;
        
            if (currentMovement != Vector3.zero)
            {
                currentMovement = Camera.main.transform.forward * currentDirection.y 
                                  + Camera.main.transform.right * currentDirection.x;
                currentMovement.y = 0f;
                _characterController.Move(currentMovement * (_currentSpeed * Time.deltaTime));
            }
        }
    }
    
    private void Rotation(InputsData inputsData)
    {
        if (!_interactionHandler.IsInteracting)
        {
            Vector2 mouseDirection = inputsData.MouseDirection;
            
            if (mouseDirection != Vector2.zero)
            {
                _startingRotation.x += mouseDirection.x * _rotationSpeed * Time.deltaTime;
                
                _startingRotation.x = Mathf.Clamp(_startingRotation.x,
                    -_minHorizontalAngleRotation, _maxHorizontalAngleRotation);
                
                transform.localRotation = Quaternion.Euler(0, _startingRotation.x, 0);
            }
        }
    }

    private void Jump(InputsData inputsData)
    {
        bool isJumping = inputsData.Isjumping;
        if (isJumping && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -4.0f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
