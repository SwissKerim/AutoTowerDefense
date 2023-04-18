using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public double Hp = 20d;
    public float AttackDamage { get; private set; } = 1f;
    public List<GameObject> EnemiesInRange = new List<GameObject>();
    private bool _isAttacking { get; set; } = false;
    private int _attackSpeedMs { get; set; } = 1000;
    private float _attackRangeRadius { get; set; } = 20;

    [SerializeField] private TowerAttackRange TowerAttackRange;
    [SerializeField] private GameObject TowerBullet;

    void Start()
    {
        TowerAttackRange.AttackRangeCollider.radius = _attackRangeRadius;
        StartCoroutine(AttackWhenEnemyInRange());
    }

    /// <summary>
    /// Create a bullet and let it fly to the enemy
    /// </summary>
    private void CreateBullet(GameObject enemy)
    {
        // Spawn a TowerBullet GameObject
        var towerBullet = Instantiate(TowerBullet, transform.position, Quaternion.identity);

        // Let the bullet fly to the enemy
        towerBullet.GetComponent<TowerBullet>().FlyToEnemy(enemy);
    }

    /// <summary>
    /// a Routine to attack the enemy
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackWhenEnemyInRange()
    {
        while (true)
        {
            if (EnemiesInRange.Count > 0)
            {
                _isAttacking = true;

                // Get the nearest enemy from the list in a var for easier access
                var nearestEnemy = GetNearestEnemy();

                while (nearestEnemy != null)
                {
                    // Face the enemy
                    transform.LookAt(nearestEnemy.transform);

                    // Spawn a bullet and let it fly to the enemy
                    CreateBullet(nearestEnemy);

                    yield return new WaitForSeconds(_attackSpeedMs / 1000);
                }

                yield return new WaitForSeconds(_attackSpeedMs / 1000);
            }
            else
            {
                _isAttacking = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void TakeDamage(double damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            // Destroy(gameObject);
            Debug.LogWarning("Tower is dead!");
        }
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }

    private GameObject GetNearestEnemy()
    {
        // Get the nearest Enemy from the current Tower position
        var nearestEnemy = EnemiesInRange
            .OrderBy(enemy => enemy != null ? Vector3.Distance(transform.position, enemy.transform.position) : 999)
            .FirstOrDefault();
        return nearestEnemy;
    }
}