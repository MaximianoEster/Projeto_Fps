using UnityEngine;

[CreateAssetMenu(fileName = "Camera Shake Data", menuName = "Data/Camera Shake/Camera Shake Data")]
public class CameraShakeData : ScriptableObject
{
    public float Delay;
    public float Intensity;
    public float Frequency;
}
