using UnityEngine;

[CreateAssetMenu(fileName = "OP_FileName Data", menuName = "Data/Object Pooling/Object Pool Data")]
public class ObjectPoolData : ScriptableObject
{
    public GameObject Prefab;
    public ItemType itemType;
    public int PrefabAmount;
}
