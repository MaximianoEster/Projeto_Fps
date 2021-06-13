using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InteractiveItem : MonoBehaviour,Iinteractable
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
    
    private bool _onCannon = default;

    private void Awake()
    {
        _virtualCamera.gameObject.SetActive(false);
    }

    public void OnInteractionPerformed(bool setActive)
    {
        _onCannon = setActive;
        _virtualCamera.gameObject.SetActive(setActive);
    }
    
    public bool OnCannon => _onCannon;
}
