using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void CreateExplosion(Cannonball originPosition)
    {
        Vfx currentExplosion = GameManager.Instance.VfxObjectPool.GetVfxFromPool(VfxType.CANNON_EXPLOSION);
        currentExplosion.transform.position = originPosition.transform.position;
        currentExplosion.transform.rotation = Quaternion.identity;
        currentExplosion.gameObject.SetActive(true);
    }
}
