using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private HeadBobData _headBobData = default;
    [SerializeField] private Transform _virtualCameraOrientation = default;

    private Vector3 _initialPos = default;
    private float _regularSpeed = default;
    
    private float _xScroll = default;
    private float _yScroll = default;
    
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
        
        _finalOffset = Vector3.zero;

        _regularSpeed = _headBobData.RegularSpeed;
        _initialPos = _headBobData.RegularCameraPosition;
    }

    public void MoveHeadBob(Vector2 movement)
    {
        ScrollFpsHead(movement);
            
        _virtualCameraOrientation.localPosition = Vector3.Lerp(_virtualCameraOrientation.localPosition,
            (Vector3.up * _virtualCameraOrientation.localPosition.y) + _finalOffset,
            Time.deltaTime * _regularSpeed);
    }

    public void ResetHeadBob()
    {
        _virtualCameraOrientation.localPosition = Vector3.Lerp(_virtualCameraOrientation.localPosition,
            _initialPos, Time.deltaTime * _regularSpeed);
    }
    
    private void ScrollFpsHead(Vector2 input)
    {
        Vector2 directions = input;
        
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
}
