using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void CreateExplosion(Cannonball originPosition)
    {
        Vfx currentExplosion = GameManager.Instance.VfxManager.GetVfxFromPool(VfxType.EXPLOSION_SIMPLE);
        currentExplosion.transform.position = originPosition.transform.position;
        currentExplosion.transform.rotation = Quaternion.identity;
        currentExplosion.gameObject.SetActive(true);
    }
}
