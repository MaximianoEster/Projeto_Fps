using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    //[SerializeField] private 
    private void Awake()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        InitializeWeaponSettings();
    }
}
