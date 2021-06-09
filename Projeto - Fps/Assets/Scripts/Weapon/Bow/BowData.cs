using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow Data", menuName = "Data/Weapon/Bow/Bow Data")]
public class BowData : ScriptableObject
{
    public float TimeToReload;
    public float ArrowLaunchForce;
}
