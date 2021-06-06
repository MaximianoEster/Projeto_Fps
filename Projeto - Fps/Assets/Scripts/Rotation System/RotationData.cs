using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rotation Data", menuName = "Data/Rotation System/Global Rotation Data")]
public class RotationData : ScriptableObject
{
    [Header("Clamp Verticals Angles")]
    public float VerticalAngleMax;
    public float VerticalAngleMin;
    
    [Space,Header("Clamp Horizontal Angles")]
    public float HorizontalAngleMax;
    public float HorizontalAngleMin;
    
    [Space,Header("Angles Speed")]
    public float HorizontalSpeed;
    public float VerticalSpeed;
}
