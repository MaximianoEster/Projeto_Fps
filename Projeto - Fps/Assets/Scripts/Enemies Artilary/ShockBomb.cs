using UnityEngine;

public class ShockBomb : MonoBehaviour
{
    [SerializeField] private ShockBombData _shockBombData = default;
    [SerializeField] private Rigidbody _rigidbody = default;
    
    private float _launchUpForce = default;
    private float _launchForwardForce = default;
    private float _radius = default;
    private float _actionTime = default;
    private int _maxColliders = default;
    private LayerMask _layerMask = default;
    private ItemType _itemType = default;
    
    private bool _isHit = false;
    private bool _isInitialized = false;
    

    public void InitializeShockBomb()
    {
        _launchForwardForce = _shockBombData.LaunchForwardForce;
        _launchUpForce = _shockBombData.LaunchUpForce;
        _radius = _shockBombData.Radius;
        _maxColliders = _shockBombData.MaxColliders;
        _layerMask = _shockBombData.LayerMaskCollision;
        _actionTime = _shockBombData.ActionTime;
        _itemType = _shockBombData.Type;
        
        _isInitialized = true;
        Debug.Log("Bomb is initialized");
    }
    
    public void LaunchBomb(Vector3 force)
    {
        _isHit = false;
        _rigidbody.velocity = new Vector3(force.x, 0, force.z) * _launchForwardForce;
        _rigidbody.AddForce(Vector3.up * _launchUpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!_isHit)
        {
            _isHit = true;
            
            //GameObject currentVfx = GameManager.Instance.ObjectPoolManager.GetObjectFromPool(ItemType.SHOCK_BOMB);
            //currentVfx.transform.localPosition = this.transform.position;
            //currentVfx.transform.localRotation = Quaternion.identity;
            //currentVfx.gameObject.SetActive(true);
            EnableShockWave();
            GameManager.Instance.ObjectPoolManager.ReturnToPool(_itemType, gameObject);
        }
    }

    private void EnableShockWave()
    {
        Collider[] hitColliders = new Collider[_maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, hitColliders, _layerMask);
        if (numColliders != 0)
        {
            if (hitColliders[0].TryGetComponent(out IStop other))
            {
                other.OnTakeShocked(_actionTime);
                GameManager.Instance.ObjectPoolManager.ReturnToPool(_itemType, gameObject);
            }
        }
    }

    public bool IsInitialized => _isInitialized;
}
