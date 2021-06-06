using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon Data", menuName = "Data/Weapon/General")]
public class WeaponData : ScriptableObject
{
    public WeaponType Type;
    public float Damage;
}
