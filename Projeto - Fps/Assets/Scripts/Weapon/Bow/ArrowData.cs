using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Data", menuName = "Data/Weapon/Bow/Arrow Data")]
public class ArrowData : ScriptableObject
{
    public float Damage;
    public ArrowType Type;
}
