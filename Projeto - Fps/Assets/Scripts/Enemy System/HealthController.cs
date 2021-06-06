using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float _maxHealth = default;
    private float _currentHealth = default;
    private bool _isHit = default;

    public void InitializeHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (_isHit)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Debug.Log("Morreu");
            }
        }
    }
}
