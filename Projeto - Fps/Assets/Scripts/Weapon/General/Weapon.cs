using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public delegate void WeaponActionHandler();
    public event WeaponActionHandler OnAttackPerformed;
    
    [Header("Weapon Settings")]
    [SerializeField] private WeaponData _weaponData;

    [Space, Header("Weapon Position")] 
    [SerializeField] protected Transform _anchor = default;
    [SerializeField] protected Transform _defaultPosition = default;
    [SerializeField] protected Transform _aimPosition = default;
    
    protected WeaponType _weaponType = default;
    protected float _damage = default;
    
    public virtual void InitializeWeaponSettings()
    {
        _weaponType = _weaponData.Type;
        _damage = _weaponData.Damage;
    }

    public void OnAttack()
    {
        OnAttackPerformed?.Invoke();
    }

    public void SetCurrentPosition(bool isAiming)
    {
        Transform currentPosition;
        if (!isAiming)
        {
            currentPosition = _defaultPosition;
            
        }
        else
        {
            currentPosition = _aimPosition;
            
        }
        _anchor.transform.position = currentPosition.position;
        
    }
    
    public WeaponType WeaponType => _weaponType;
}
