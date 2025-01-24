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
        health *= 2f;

        // Stronger attack
        damageAmount *= 1.5f;
    }

    // You can override other methods if needed
}
