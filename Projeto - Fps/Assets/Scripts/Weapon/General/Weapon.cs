using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    protected WeaponType _type = default;
    protected float _damage = default;
    
    public void InitializeWeaponSettings()
    {
        _type = _weaponData.Type;
        _damage = _weaponData.Damage;
    }
}
