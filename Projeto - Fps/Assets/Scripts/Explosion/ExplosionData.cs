using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion Data", menuName = "Data/Explosion/Explosion Data")]
public class ExplosionData : ScriptableObject
{
    public Explosion ExplosionPrefab;
    public int ExplosionAmount;
    private List<Explosion> _explosionsList = new List<Explosion>();

    public void InitializePool()
    {
        CreatePool();
    }
    
    public Explosion GetPooledExplosion()
    {
        for (int i = 0; i < ExplosionAmount; i++)
        {
            if (!_explosionsList[i].gameObject.activeInHierarchy)
            {
                return _explosionsList[i];
            }
        }
        return null;
    }
    
    private void CreatePool()
    {
        _explosionsList = new List<Explosion>();
        Explosion temp;
        
        for (int i = 0; i < ExplosionAmount; i++)
        {
            temp = Instantiate(ExplosionPrefab);
            temp.gameObject.SetActive(false);
            _explosionsList.Add(temp);
        }
    }
}
