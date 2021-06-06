using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Raycast Data", menuName = "Data/Raycast/Raycast Data")]
public class SphereCastData : ScriptableObject
{
    public float RayDistance;
    public float RayRadius;
    public LayerMask InteractableLayerMask;
}
