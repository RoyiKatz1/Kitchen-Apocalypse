public class BigEnemy : Enemy
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
    }

    // You can override other methods if needed
}
