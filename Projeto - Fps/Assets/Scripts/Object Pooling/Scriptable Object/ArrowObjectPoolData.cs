using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Object Pool Data", menuName = "Data/Object Pooling/Arrow Object Pool Data")]
public class ArrowObjectPoolData : ScriptableObject
{
   public List<Arrow> ArrowsPrefabsList;
   public int ArrowAmount;
}
