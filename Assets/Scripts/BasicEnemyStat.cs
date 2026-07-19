using UnityEngine;

[CreateAssetMenu(fileName = "BasicEnemyStat", menuName = "Stats/BasicEnemyStats")]
public class BasicEnemyStat : ScriptableObject
{
    public int enemyHealth = 20;
    public int enemyDamage = 5; 
}
