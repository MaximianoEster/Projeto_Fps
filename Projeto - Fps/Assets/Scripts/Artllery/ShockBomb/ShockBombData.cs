using UnityEngine;

[CreateAssetMenu(fileName = "Shock Bomb Data", menuName = "Data/Artllery/Shock Bomb Data")]
public class ShockBombData : ScriptableObject
{
    public ItemType Type;
    public LayerMask LayerMaskCollision;
    public float Radius;
    public float LaunchUpForce;
    public float LaunchForwardForce;
    public float ActionTime;
    public int MaxColliders;
}
