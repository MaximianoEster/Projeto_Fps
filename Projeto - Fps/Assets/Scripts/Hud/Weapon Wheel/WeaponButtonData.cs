using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon Button Data", menuName = "Data/HUD/Weapon Button Data")]
public class WeaponButtonData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public WeaponType Type;
}
