using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vfx : MonoBehaviour
{
    [SerializeField] private VfxData _vfxData = default;
    private VfxType _vfxType = default;

    public void InitializeSettings()
    {
        _vfxType = _vfxData.Type;
    }
    

    public VfxType Type => _vfxType;
}
