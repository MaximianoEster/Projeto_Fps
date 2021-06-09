using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponsList = default;
    private int _weaponIndex = 0;

    private void Start()
    {
        
    }

    public void DisableWeapons(bool enable)
    {
        for (int i = 0; i < _weaponsList.Count; i++)
        {
            _weaponsList[i].gameObject.SetActive(enable);
        }
    }

    public void ChangeWeapon()
    {
        if (_weaponsList != null)
        {
            for (int i = 0; i < _weaponsList.Count; i++)
            {
                _weaponsList[i].gameObject.SetActive(false);
            }
            
            _weaponsList[_weaponIndex].gameObject.SetActive(true);
        }
    }

    private void ScrollCheck()
    {
        float scrollValue = 0;
        if (scrollValue > 0)
        {
            _weaponIndex++;
            if (_weaponIndex >= _weaponsList.Count)
            {
                _weaponIndex = 0;
            }
            
            ChangeWeapon();
        }
    }
}
