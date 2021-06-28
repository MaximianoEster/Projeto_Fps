using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public delegate void ArrowHandler(Arrow arrowInstance);
    public event ArrowHandler DisableArrow;
    
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.TryGetComponent(out IDamageble damage))
        {
            damage.OnTakeDamage(_damage);
            
            /*
            Vfx currentVfx = GameManager.Instance.ObjectPoolManager.VfxObjectPool.GetVfxFromPool(VfxType.ARROW_HIT);
            currentVfx.transform.localPosition = col.transform.position;
            currentVfx.transform.localRotation = Quaternion.identity;
            currentVfx.gameObject.SetActive(true);
            */
            
            DisableArrow?.Invoke(this);
        }
    }

    public ArrowType Type => _type;
}
