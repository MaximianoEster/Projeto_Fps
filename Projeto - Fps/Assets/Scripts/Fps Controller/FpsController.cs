using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class FpsController : MonoBehaviour, IDetectable, IStop
{
    [SerializeField] private HeadBob _headBob = default;
    [SerializeField] private CharacterController _characterController = default;
    [SerializeField] private FpsData _fpsData = default;
    [SerializeField] private InteractionHandler _interactionHandler = default;
    [SerializeField] private RotationData _rotationData = default;
    [SerializeField] private WeaponManager _weaponManager = default;
    [SerializeField] private Transform _cameraPivot = default;

    [SerializeField] private FieldOfView _fov = default;
    
    private float _regularSpeed = default;
    private float _moveBackardsSpeed = default;
    private float _moveSideSpeed = default;
    private bool _isGrounded = default;
    private bool _isStopped = false;
    
    private float _jumpHeight = default;
    private float _gravity = default;

    private Vector3 _startingRotation = Vector3.zero;
    private Vector3 _velocity = default;

    private float _minVerticalAngleRotation = default;
    private float _maxVerticalAngleRotation = default;
    private float _rotationSpeed = default;
    private float _verticalRotationSpeed = default;
    
    private float _currentSpeed = default;

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
        
        _rotationSpeed = _rotationData.HorizontalSpeed;
        _verticalRotationSpeed = _rotationData.VerticalSpeed;
        
        _minVerticalAngleRotation = _rotationData.VerticalAngleMin;
        _maxVerticalAngleRotation = _rotationData.VerticalAngleMax;
        
        _headBob.InitializeSettings();
    }

    private void Movement(InputsData inputsData)
    {
        if (!_interactionHandler.IsInteracting && !_isStopped)
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
                _headBob.MoveHeadBob(currentDirection);
            }
            
            else
            {
                _headBob.ResetHeadBob();
            }
            
            //_fov.Scan();
        }
    }
    
    private void Rotation(InputsData inputsData)
    {
        if (!_interactionHandler.IsInteracting && !_isStopped)
        {
            Vector2 mouseDirection = inputsData.MouseDirection;
            
            if (mouseDirection != Vector2.zero)
            {
                _startingRotation.y += mouseDirection.y * _verticalRotationSpeed * Time.deltaTime;
                _startingRotation.x = mouseDirection.x * _rotationSpeed * Time.deltaTime;
                
                _startingRotation.y = Mathf.Clamp(_startingRotation.y,
                    -_minVerticalAngleRotation, _maxVerticalAngleRotation);
                
                transform.Rotate(Vector3.up * _startingRotation.x);
                
                _cameraPivot.localRotation = Quaternion.Euler(-_startingRotation.y, 
                    _cameraPivot.localRotation.y, _cameraPivot.localRotation.z);
                
                _weaponManager.transform.localRotation = Quaternion.Euler(-_startingRotation.y, 
                    _weaponManager.transform.localRotation.y, _weaponManager.transform.localRotation.z);
            }
        }
    }

    private void Jump(InputsData inputsData)
    {
        bool isJumping = inputsData.Isjumping;
        if (_isGrounded && isJumping && !_isStopped)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -4.0f * _gravity);
           
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void OnTakeShocked(float timeToEnable)
    {
        _isStopped = true;
        
        StartCoroutine(EnableMovement());
        IEnumerator EnableMovement()
        {
            yield return new WaitForSeconds(timeToEnable);
            _isStopped = false;
        }
    }
}
