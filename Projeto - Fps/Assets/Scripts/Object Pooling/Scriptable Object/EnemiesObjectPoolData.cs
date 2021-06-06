using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesObjectPoolData", menuName = "Data/Object Pooling/Enemies Object Pool Data")]
public class EnemiesObjectPoolData : ScriptableObject
{
    public int EnemiesAmount;
    
    public List<EnemyController> _skeletonsList = new List<EnemyController>();
    public List<EnemyController> _knightList = new List<EnemyController>();
}
