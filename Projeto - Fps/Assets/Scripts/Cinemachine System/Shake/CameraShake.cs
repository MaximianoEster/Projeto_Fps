using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = default;
    [SerializeField] private CameraShakeData _cameraShakeData = default;
    
    private CinemachineBasicMultiChannelPerlin _cmMultiChannelPerlin = default;
    private float _delay = default;
    private float _intensity = default;
    private float _frequency = default;
    
    private void Awake()
    {
        InitializeSettings();
    }
    
    public void ShakeCamera()
    {
        _cmMultiChannelPerlin.m_AmplitudeGain = _intensity;
        _cmMultiChannelPerlin.m_FrequencyGain = _frequency;

        StartCoroutine(TimerShake());
    }

    private void InitializeSettings()
    {
        _cmMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent
            <CinemachineBasicMultiChannelPerlin>();

        _delay = _cameraShakeData.Delay;
        _frequency = _cameraShakeData.Frequency;
        _intensity = _cameraShakeData.Intensity;
    }
    
    private IEnumerator TimerShake()
    {
        yield return new WaitForSeconds(_delay);
        _cmMultiChannelPerlin.m_AmplitudeGain = 0;
        _cmMultiChannelPerlin.m_FrequencyGain = 0;
    }
}
