using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private RotationData rotationData = default;
    
    private Vector2 _mouseDirection = default;
    private Vector3 _startingRotation = default;
    
    private float _clampVerticalAngleMax = default;
    private float _clampVerticalAngleMin = default;
    
    private float _clampHorizontalAngleMax = default;
    private float _clampHorizontalAngleMin = default;
    
    private float _horizontalSpeed = default;
    private float _verticalSpeed = default;
    
    protected override void Awake()
    {
        base.Awake();
        InitializePovSettings();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += UpdateMousePosition;
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                
                if (_startingRotation == null)
                {
                    _startingRotation = transform.localRotation.eulerAngles;
                }
                
                _startingRotation.x += _mouseDirection.x * _horizontalSpeed * Time.deltaTime;
                _startingRotation.y += _mouseDirection.y * _verticalSpeed * Time.deltaTime;

                _startingRotation.y = Mathf.Clamp(_startingRotation.y,
                    -_clampVerticalAngleMin, _clampVerticalAngleMax);
                
                
                _startingRotation.x = Mathf.Clamp(_startingRotation.x,
                    -_clampHorizontalAngleMin, _clampHorizontalAngleMax);
                
                
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, 
                    _startingRotation.x, 0f);
            }
        }
    }

    private void InitializePovSettings()
    {
        _clampVerticalAngleMax = rotationData.VerticalAngleMax;
        _clampVerticalAngleMin = rotationData.VerticalAngleMin;
        
        _clampHorizontalAngleMax = rotationData.HorizontalAngleMax;
        _clampHorizontalAngleMin = rotationData.HorizontalAngleMin;
        
        _horizontalSpeed = rotationData.HorizontalSpeed;
        _verticalSpeed = rotationData.VerticalSpeed;
    }

    private void UpdateMousePosition(InputsData inputsData)
    {
        _mouseDirection = inputsData.MouseDirection;
    }
}
