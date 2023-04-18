using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Tower TargetTower;
    [SerializeField] public GameController GameController;
    [Header("Base")] [SerializeField] private protected double Hp = 5;
    [SerializeField] private protected double Speed = 1;
    [Header("Attack")] [SerializeField] private protected double Damage = 2;
    [SerializeField] private protected double Armor = 0;
    [SerializeField] private protected int AttackSpeedMs = 2000;
    [SerializeField] private protected double AttackRange = 1;
    private protected bool isAttacking;

    public virtual void Start()
    {
        // TargetTower = GameObject.FindWithTag("Player").GetComponent<Tower>();
    }

    public virtual void TakeDamage(double attackDamage)
    {
        Hp -= attackDamage;
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public virtual void IncreaseDamage(double factor)
    {
        Damage *= (1 + factor);
    }

    public void IncreaseHp(double hpFactor)
    {
        Hp *= (1 + hpFactor);
    }

    public void IncreaseSpeed(double speedFactor)
    {
        Speed *= (1 + speedFactor);
    }

    public void IncreaseArmor(double armorFactor)
    {
        Armor *= (1 + armorFactor);
    }

    /// <summary>
    /// Check if the enemy is dead
    /// </summary>
    /// <returns></returns>
    public virtual bool IsDead()
    {
        return Hp <= 0;
    }

    /// <summary>
    /// a Routine to attack the enemy
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator AttackRoutine()
    {
        isAttacking = true;
        Attack();
        yield return new WaitForSeconds(AttackSpeedMs / 1000);
        isAttacking = false;
    }

    protected virtual IEnumerator AttackWhenInDistance()
    {
        while (!IsDead() || TargetTower != null || !TargetTower.IsDead())
        {
            if (Vector3.Distance(transform.position, TargetTower.transform.position) <= AttackRange && !isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    /// <summary>
    /// Move to enemy
    /// </summary>
    protected virtual void MoveToEnemy()
    {
        Debug.Log(TargetTower.IsDead());

        if (TargetTower == null || TargetTower.IsDead())
            return;

        Debug.Log("Move to enemy");

        // Move towards the tower until it reaches the attack range
        if (Vector3.Distance(transform.position, TargetTower.transform.position) > AttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetTower.transform.position,
                (float)Speed * Time.deltaTime);
        }
    }

    protected abstract void Attack();
    protected abstract void Update();

    private void OnDestroy()
    {
        TargetTower.EnemiesInRange.Remove(gameObject);
    }
}