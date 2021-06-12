using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private WeaponData _weaponData;

    [Space, Header("Weapon Position")] 
    [SerializeField] protected Transform _anchor = default;
    [SerializeField] protected Transform _defaultPosition = default;
    [SerializeField] protected Transform _aimPosition = default;
    
    protected WeaponType _type = default;
    protected float _damage = default;
    
    public void InitializeWeaponSettings()
    {
        _type = _weaponData.Type;
        _damage = _weaponData.Damage;
    }
    
    public WeaponType Type => _type;
}
