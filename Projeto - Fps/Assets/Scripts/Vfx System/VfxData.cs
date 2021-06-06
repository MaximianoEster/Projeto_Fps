using UnityEngine;

[CreateAssetMenu(fileName = "VfxData", menuName = "Data/Vfx/Vfx Data")]
public class VfxData : ScriptableObject
{
    public VfxType Type;
    public float LifeTime;
}
