using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageble
{
    public delegate void HealthHandler();
    public event HealthHandler OnHealthIsOver;
    
    private float _maxHealth = default;
    private float _currentHealth = default;
    private bool _isHit = false;
    
    public void InitializeHealth(float healthAmount)
    {
        _maxHealth = healthAmount;
        _currentHealth = _maxHealth;
    }
    
    public void OnTakeDamage(float damageAmount)
    {
        if (!_isHit)
        {
            _currentHealth -= damageAmount;
            _isHit = true;
            
        }

        _isHit = false;
        if (_currentHealth <= 0)
        {
            OnHealthIsOver?.Invoke();
        }
    }
}
