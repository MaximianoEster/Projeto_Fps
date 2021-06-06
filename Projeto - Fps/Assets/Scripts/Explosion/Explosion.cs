using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float DELAY = 0.7f;
    public void EnableExplosion()
    {
        gameObject.SetActive(true);
        StartCoroutine(DisableExplosion());
    }

    private IEnumerator DisableExplosion()
    {
        yield return  new WaitForSeconds(DELAY);
        gameObject.SetActive(false);
    }
}
