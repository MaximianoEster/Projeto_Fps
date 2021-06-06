using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private HeadBobData _headBobData = default;
    [SerializeField] private Transform _virtualCameraOrientation = default;
    
    private float _xScroll = default;
    private float _yScroll = default;
    
    private bool _resetted = false;
    private Vector3 _finalOffset = default;
    
    private float _moveBackwardsFrequencyMultiplier = default;
    private float _moveSideFrequencyMultiplier = default;
    private float _runAmplitudeMultiplier = default;
    private float _runFrequencyMultiplier = default;
    
    private float _regularFrequencyMultiplier = default;
    private float _additionalMultiplier = default;
    
    private float _xFrequency = default;
    private float _yFrequency = default;
    
    private float _xAmplitude = default;
    private float _yAmplitude = default;
    
    private AnimationCurve _xCurve = default;
    private AnimationCurve _yCurve = default;
    
    private void Awake()
    {
        InitializeSettings();
    }
    
    private void Start()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed += HeadBobUpdate;
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAnyKeyPressed -= HeadBobUpdate;
    }
    
    public void InitializeSettings()
    {
        _moveBackwardsFrequencyMultiplier = _headBobData.MoveBackwardsFrequencyMultiplier;
        _moveSideFrequencyMultiplier = _headBobData.MoveSideFrequencyMultiplier;

        _runAmplitudeMultiplier = _headBobData.runAmplitudeMultiplier;
        _runFrequencyMultiplier = _headBobData.runFrequencyMultiplier;

        _regularFrequencyMultiplier = _headBobData.RegularFrequencyMultiplier;

        _xFrequency = _headBobData.xFrequency;
        _yFrequency = _headBobData.yFrequency;

        _xAmplitude = _headBobData.XAmplitude;
        _yAmplitude = _headBobData.YAmplitude;

        _xCurve = _headBobData.XCurve;
        _yCurve = _headBobData.YCurve;
        
        _xScroll = 0;
        _yScroll = 0;
        
        _resetted = false;
        _finalOffset = Vector3.zero;
    }

    private void HeadBobUpdate(InputsData inputsData)
    {
        Vector2 currentDirection = inputsData.KeyboardDirection;
        Vector3 currentMovement = new Vector3(currentDirection.x, 0f, currentDirection.y);
        
        if (currentMovement != Vector3.zero)
        {
            ScrollFpsHead(currentDirection);
            
            _virtualCameraOrientation.localPosition = Vector3.Lerp(_virtualCameraOrientation.localPosition,
                (Vector3.up * _virtualCameraOrientation.localPosition.y) + _finalOffset,
                Time.deltaTime * 1.8f);
        }
        else
        {
            if (_resetted == false)
            {
                ResetFpsHead();
            }

            _virtualCameraOrientation.localPosition = Vector3.Lerp(_virtualCameraOrientation.localPosition,
                new Vector3(0f, _virtualCameraOrientation.localPosition.y, 0f), Time.deltaTime * 1.8f);
        }
    }
    
    private void ScrollFpsHead(Vector2 input)
    {
        Vector2 directions = input;
        _resetted = false;

        _xScroll += Time.deltaTime * _xFrequency * _runFrequencyMultiplier;
        _yScroll += Time.deltaTime * _yFrequency * _runFrequencyMultiplier;

        float xValue;
        float yValue;

        xValue = _xCurve.Evaluate(_xScroll);
        yValue = _yCurve.Evaluate(_yScroll);
        
        _additionalMultiplier = Mathf.Abs(directions.x) > 0 ? _moveSideFrequencyMultiplier :
            directions.y == -1 ? _moveBackwardsFrequencyMultiplier : _regularFrequencyMultiplier;
        
        _finalOffset.x = xValue * _xAmplitude * _runAmplitudeMultiplier * _additionalMultiplier;
        _finalOffset.y = yValue * _yAmplitude * _runAmplitudeMultiplier * _additionalMultiplier;
    }

    private void ResetFpsHead()
    {
        _resetted = true;

        _xScroll = 0f;
        _yScroll = 0f;
        
        _finalOffset = Vector3.zero;
    }
}
