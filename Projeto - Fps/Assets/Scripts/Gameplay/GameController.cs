using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController = default;
    [SerializeField] private WeaponManager _fpsWeaponManager = default;

    private void Start()
    {
        InitializePlayerSettings();
    }
    private void InitializePlayerSettings()
    {
        _canvasController.UpdateWeapon += NotifyPlayer;
        
    }

    private void NotifyPlayer(WeaponType type)
    {
        _fpsWeaponManager.ChangeWeapon(type);
    }
}
