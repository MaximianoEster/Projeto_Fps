using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vfx : MonoBehaviour
{
    public delegate void VfxHandler(Vfx vfxObj);
    public event VfxHandler DisableVfx;
    
    [SerializeField] private VfxData _vfxData = default;
    private VfxType _vfxType = default;
    private ParticleSystem.MainModule _mainModule = default;
    
    public void InitializeSettings()
    {
        _vfxType = _vfxData.Type;
        
        _mainModule = GetComponent<ParticleSystem>().main;
        _mainModule.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        DisableVfx?.Invoke(this);
    }
    
    public VfxType Type => _vfxType;
}
