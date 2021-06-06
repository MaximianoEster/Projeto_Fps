using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FpsData", menuName = "Data/Fps Controller/FpsData")]
public class FpsData : ScriptableObject
{
    [Header("Locomotion")]
    public float RegularSpeed;
    public float MoveBackardsSpeed;
    public float MoveSideSpeed;
    
    [Space,Header("Jump")]
    public float JumpHeight;
    public float Gravity;
    
    [Space,Header("Rotation")]
    public float RotationSpeed;
}
