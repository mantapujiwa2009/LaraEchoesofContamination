using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Stats/PlayerStats")]
public class PlayerStat : ScriptableObject
{
    public int playerHealth = 100;
    public int playerDamage = 10;
}
