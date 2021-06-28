using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private ObjectPoolManager _objectPoolManager = default;
    public List<Transform> CheckPoints = new List<Transform>();
    
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
        _objectPoolManager.InitializeObjectPool();
        
    }

    public ObjectPoolManager ObjectPoolManager => _objectPoolManager;

    public InputManager InputManager => _inputManager;
}
