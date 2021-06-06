using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = default;
    [SerializeField] private Transform _tip = default;

    private bool _isStopped = true;
    private Vector3 _lastPosition = Vector3.zero;
    
    public void InitializeArrow()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    public void Fire(float speed)
    {
        gameObject.transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(gameObject.transform.forward * speed);
    }
    
    //Old Code
    public void OnFire(Vector3 speed)
    {
        _rigidbody.AddForce(speed, ForceMode.VelocityChange);
    }
}
