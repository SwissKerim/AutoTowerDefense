using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    public double Hp = 20;
    public List<GameObject> EnemiesInRange = new List<GameObject>();

    public bool IsAttacking = false;
    public int AttackSpeedMs = 1000;
    public float AttackDamage = 1f;
    public float AttackRangeRadius = 20;

    [SerializeField] private TowerAttackRange TowerAttackRange;
    [SerializeField] private GameObject TowerBullet;

    // Start is called before the first frame update
    void Start()
    {
        TowerAttackRange.AttackRangeCollider.radius = AttackRangeRadius;
        StartCoroutine(AttackWhenEnemyInRange());
    }

    // Update is called once per frame
    void Update()
    {
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
                IsAttacking = true;

                // Get the nearest enemy from the list in a var for easier access
                var nearestEnemy = GetNearestEnemy();

                // Face the enemy
                transform.LookAt(nearestEnemy.transform);

                // Spawn a bullet and let it fly to the enemy
                CreateBullet(nearestEnemy);

                yield return new WaitForSeconds(AttackSpeedMs / 1000);
            }
            else
            {
                IsAttacking = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void TakeDamage(double damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
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
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).FirstOrDefault();
        return nearestEnemy != null ? nearestEnemy : null;
    }
}