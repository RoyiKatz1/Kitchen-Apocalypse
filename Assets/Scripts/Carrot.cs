public class Carrot : Enemy
{
    // Carrot specific implementation
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Additional Carrot-specific damage handling
    }
}