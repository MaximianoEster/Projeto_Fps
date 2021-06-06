using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public EnemyType EnemyType;
    public float DamageAmount;
    public float HealthAmount;
}
