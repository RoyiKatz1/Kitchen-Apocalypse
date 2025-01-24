using UnityEngine;

public class Tomato : Enemy
{
    public float sizeMultiplier = 1.5f;
    public float explosionRadius = 2f;
    public int explosionDamage = 40;
    private Animator animator;


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

        animator = GetComponent<Animator>();

    }

    protected override void Die()
    {
        Explode();
        Debug.Log($"{GetType().Name} died");
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue, transform.position);
        }
        else
        {
            Debug.LogWarning("ScoreManager instance not found. Score not added.");
        }

        Destroy(gameObject, 3f);

    }

    public void Explode()
    {
        animator.SetTrigger("Explosion");

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in hitObjects)
        {
            if (obj.CompareTag("Player"))
            {
                Player player = obj.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(explosionDamage);
                    Debug.Log($"{GetType().Name} exploded and dealt {explosionDamage} explosionDamage to the player");
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
