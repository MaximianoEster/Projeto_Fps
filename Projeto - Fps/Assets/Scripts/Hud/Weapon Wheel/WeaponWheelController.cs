using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class WeaponWheelController : MonoBehaviour
{
    public delegate void WeaponWheelHandler(WeaponType buttonType);
    public event WeaponWheelHandler OnWeaponSelected;
    
    [SerializeField] private List<WeaponWheelButtonController> _buttonsList;
    [SerializeField] private TextMeshProUGUI _descriptionText = default;
    [SerializeField] private TextMeshProUGUI _nameItem = default;
    [SerializeField] private Image _weaponDisplay = default;

    private string _currentWeaponDescription = default;
    private string _currentWeaponName = default;
    private Sprite _currentWeaponIcon = default;
    private WeaponType _type;
    
    public void InitializeSettings()
    {
        for (int i = 0; i < _buttonsList.Count; i++)
        {
            _buttonsList[i].GetButtonData += UpdateWeaponSelected;
            _buttonsList[i].OnButtonPressed += ChangeInterface;
            _buttonsList[i].InitializeSettings();
        }
    }

    private void UpdateWeaponSelected(WeaponButtonStruct buttonData)
    {
        _currentWeaponDescription = buttonData.Description;
        _currentWeaponIcon = buttonData.Icon;
        _currentWeaponName = buttonData.Name;

        _type = buttonData.Type;
        _descriptionText.text = _currentWeaponDescription;
        _nameItem.text = _currentWeaponName;
    }

    private void ChangeInterface()
    {
        _weaponDisplay.sprite = _currentWeaponIcon;
        OnWeaponSelected?.Invoke(_type);
    }
}
