using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private VfxManager vfxManager = default;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeDefaultGameSettings();
    }

    private void InitializeDefaultGameSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public InputManager InputManager => _inputManager;

    public VfxManager VfxManager => vfxManager;
}
