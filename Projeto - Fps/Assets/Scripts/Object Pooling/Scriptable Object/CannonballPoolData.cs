using UnityEngine;

[CreateAssetMenu(fileName = "CannonballPoolData", menuName = "Data/Object Pooling/Cannonball Object Pool Data")]
public class CannonballPoolData : ScriptableObject
{
    public Cannonball CannonballPrefab;
    public int CannonballAmount;
}
