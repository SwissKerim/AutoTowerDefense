using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject SuicideEnemyPrefab;

    public void SpawnEnemy(double damageFactor, double hpFactor, double armorFactor)
    {
        var enemy = Instantiate(SuicideEnemyPrefab, transform.position, Quaternion.identity)
            .GetComponent<SuicideEnemy>();
        enemy.IncreaseDamage(damageFactor);
        enemy.IncreaseHp(hpFactor);
        enemy.IncreaseArmor(armorFactor);
    }
}