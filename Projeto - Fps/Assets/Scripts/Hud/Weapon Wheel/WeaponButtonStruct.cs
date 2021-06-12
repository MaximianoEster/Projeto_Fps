using UnityEngine;
using UnityEngine.UI;

public struct WeaponButtonStruct
{
    public string Description;
    public string Name;
    public Sprite Icon;
    public WeaponType Type;

    public WeaponButtonStruct(string description, string name, Sprite icon, WeaponType type)
    {
        Description = description;
        Name = name;
        Icon = icon;
        Type = type;
    } 
}
