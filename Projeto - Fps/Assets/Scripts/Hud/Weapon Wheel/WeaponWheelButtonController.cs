using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponWheelButtonController : MonoBehaviour
{
  public delegate void ButtonDataHandler(WeaponButtonStruct buttonData);
  public event ButtonDataHandler GetButtonData;
  
  public delegate void ButtonActionHandler();
  public event ButtonActionHandler OnButtonPressed;
  
  private const string HOVER = "Hover";

  [SerializeField] private WeaponButtonData _weaponButtonData = default;
  [SerializeField] private Animator _animator = default;
  
  private WeaponButtonStruct _currentStruct = default;
  private bool _isSelected = false;
  
  private string _Description = default;
  private string _itemName = default;
  private Sprite _icon = default;
  private WeaponType _type = default;
  
  public void InitializeSettings()
  {
    _Description = _weaponButtonData.Description;
    _itemName = _weaponButtonData.Name;
    _icon = _weaponButtonData.Icon;
    _type = _weaponButtonData.Type;
  }
  
  public void Selected()
  {
    _isSelected = true;
    OnButtonPressed?.Invoke();
  }

  public void Deselected()
  {
    _isSelected = false;
  }

  public void HoverEnter()
  {
    _animator.SetBool(HOVER, true);
    GetButtonData?.Invoke(CreateStruct());
  }
  
  public void HoverExit()
  {
    _animator.SetBool(HOVER, false);
    GetButtonData?.Invoke(ResetStruct());
  }

  private WeaponButtonStruct CreateStruct()
  {
    _currentStruct = new WeaponButtonStruct(_Description, _itemName, _icon, _type);
    return _currentStruct;
  }

  private WeaponButtonStruct ResetStruct()
  {
    return new WeaponButtonStruct(String.Empty, String.Empty, null, WeaponType.NONE);
  }
}
