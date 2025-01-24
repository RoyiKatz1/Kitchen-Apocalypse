public class Tomato : Enemy
{
    public float sizeMultiplier = 1.5f;

    void Start()
    {
        // Increase size
        transform.localScale *= sizeMultiplier;

        // Slower movement
        moveSpeed *= 0.5f;

        // More health
        health *= 4f;

        // Stronger attack
        damageAmount *= 3f;

        // Slower Intervals
        damageInterval *= 2f;

        // Change scoring
        scoreValue = 50; 
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Additional Tomato-specific damage handling
    }
}
