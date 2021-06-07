using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VfxObjectPoolData", menuName = "Data/Object Pooling/Vfx Object Pool Data")]
public class VfxObjectPoolData : ScriptableObject
{
    public int VfxAmount;
    public List<Vfx> VfxPrefabsList;
}
