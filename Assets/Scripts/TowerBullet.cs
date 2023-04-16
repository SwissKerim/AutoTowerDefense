using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    // Target enemy
    private GameObject _enemy;

    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private Tower Tower;

    private double _bulletSpeed = 6;

    /// <summary>
    /// Attack the Enemy
    /// </summary>
    /// <param name="enemy">enemy</param>
    public void FlyToEnemy(GameObject enemy)
    {
        _enemy = enemy;
        Transform objectTransform = gameObject.transform;

        // Get the Vector3 of the enemy
        Vector3 enemyPosition = enemy.transform.position;

        // look at the enemy
        objectTransform.LookAt(enemyPosition);

        // Get the direction to the enemy and normalize it
        Vector3 direction = (enemyPosition - transform.position).normalized;

        // Shot the bullet to the enemy with the direction and the bullet speed 
        Rigidbody.AddForce(direction * (float)_bulletSpeed, ForceMode.Impulse);

        // Destroy the bullet after 5 seconds   
        Destroy(gameObject, 5);
    }

    /// <summary>
    /// Destroy the bullet if it hits the enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _enemy) return;

        var enemy = other.GetComponent<Enemy>();

        // Attack the nearest enemy
        enemy.TakeDamage(Tower.AttackDamage);

        Destroy(gameObject);
    }
}