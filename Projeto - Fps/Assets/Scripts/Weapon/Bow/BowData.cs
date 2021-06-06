using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow Data", menuName = "Data/Weapon/Bow/Bow Data")]
public class BowData : ScriptableObject
{
    public float TimeToRelaod;
    public float FirePower;
    public int ArrowAmount;
    public Arrow ArrowPrefab;
    
    private List<Arrow> _arrowList = new List<Arrow>();

    public void InitializeBowPool()
    {
        CreatePool();
    }
    
    public Arrow GetArrowFromPool()
    {
        for (int i = 0; i < ArrowAmount; i++)
        {
            if (!_arrowList[i].gameObject.activeInHierarchy)
            {
                return _arrowList[i];
            }
        }

        return null;
    }
    
    private void CreatePool()
    {
        _arrowList = new List<Arrow>();
        Arrow temp;
        
        for (int i = 0; i < ArrowAmount; i++)
        {
            temp = Instantiate(ArrowPrefab);
            temp.gameObject.SetActive(false);
            _arrowList.Add(temp);
        }
    }
}
