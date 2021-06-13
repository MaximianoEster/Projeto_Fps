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
    
    protected WeaponType _type = default;
    protected float _damage = default;
    
    public void InitializeWeaponSettings()
    {
        _type = _weaponData.Type;
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

        LeanTween.value(_anchor.gameObject, _anchor.transform.position,
            currentPosition.position, .5f).setOnUpdate((Vector3 pos) =>
        {
            _anchor.transform.position = pos;
        });
    }
    
    public WeaponType Type => _type;
}
