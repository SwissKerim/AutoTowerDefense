public class SuicideEnemy : Enemy
{
    public override void Start()
    {
        base.Start();
        Hp = 2;
        Speed = 5;
        Damage = 1;
        
        // If the enemy is in range of the tower, attack it
        StartCoroutine(AttackWhenInDistance());
    }

    protected override void Update()
    {
        MoveToEnemy();
    }

    protected override void Attack()
    {
        TargetTower.TakeDamage(Damage);
    }
}