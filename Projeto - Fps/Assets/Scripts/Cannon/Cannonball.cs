using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public delegate void CollisionHandler(Cannonball explosionOrigin);
    public event CollisionHandler OnColliderAnotherObject;

    private bool _isHit = false;
    
    [SerializeField] private Rigidbody _rigidbody = default;
    private float _radius = 10;
    
    public void OnFire(Vector3 speed)
    {
        _rigidbody.AddForce(speed, ForceMode.VelocityChange);
        _isHit = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_isHit)
        {
            return;
        }
        OnColliderAnotherObject?.Invoke(this);
        _isHit = true;
    }
}
