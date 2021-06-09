using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private ArrowData _arrowData = default;
    [SerializeField] private Rigidbody _rigidbody = default;

    private ArrowType _type = default;
    private float _damage = default;
    
    public void InitializeArrow()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _type = _arrowData.Type;
        _damage = _arrowData.Damage;
    }

    public void Fire(float speed)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(gameObject.transform.forward * speed);
    }
    
    public ArrowType Type => _type;
}
