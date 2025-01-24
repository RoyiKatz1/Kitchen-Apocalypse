public class RegularEnemy : Enemy
{
    // RegularEnemy specific implementation
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Additional RegularEnemy-specific damage handling
    }
}