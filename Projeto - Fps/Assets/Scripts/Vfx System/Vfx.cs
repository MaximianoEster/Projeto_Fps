using UnityEngine;

public class Vfx : MonoBehaviour
{
    [SerializeField] private VfxData _vfxData = default;
    
    private ParticleSystem.MainModule _mainModule = default;
    private ItemType _type = default;
    private bool _isInitialized = false;
    
    public void InitializeVfxSettings()
    {
        _type = _vfxData.Type;
        _mainModule = GetComponent<ParticleSystem>().main;
        _mainModule.stopAction = ParticleSystemStopAction.Callback;
        _isInitialized = true;
    }

    private void OnParticleSystemStopped()
    {
       GameManager.Instance.ObjectPoolManager.ReturnToPool(_type, gameObject);
    }

    public bool IsInitialized => _isInitialized;
}
