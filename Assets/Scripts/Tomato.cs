using UnityEngine;
using System.Collections;
using System.Linq;


public class Tomato : Enemy
{
    public float sizeMultiplier = 1.5f;
    public float explosionRadius = 1f;
    public int explosionDamage = 30;
    public AudioClip explosionSound; // Assign this in the Inspector

    private Animator animator;
    private bool hasExploded = false;

    protected override void Start()
    {
        base.Start();
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
        if (!hasExploded)
        {
            StartCoroutine(ExplodeCoroutine());
        }
    }

    private IEnumerator ExplodeCoroutine()
    {
        Debug.Log("Starting explosion coroutine");
        hasExploded = true;
        Explode();

        // Disable the collider to allow walking through during animation
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Wait for the explosion animation to finish
        yield return new WaitForSeconds(3f); // Adjust this time to match your animation length

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue, transform.position);
        }
        else
        {
            Debug.LogWarning("ScoreManager instance not found. Score not added.");
        }

        Destroy(gameObject);
    }

    public void Explode()
    {
        animator.SetTrigger("Explosion");

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Debug.Log($"Explosion at position: {transform.position}, Radius: {explosionRadius}");

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Debug.Log($"Objects detected in explosion: {hitObjects.Length}");

        foreach (Collider2D obj in hitObjects)
        {
            Debug.Log($"Detected object: {obj.name}, Tag: {obj.tag}");
            if (obj.CompareTag("Player"))
            {
                Player player = obj.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(explosionDamage);
                    Debug.Log($"{GetType().Name} exploded and dealt {explosionDamage} explosion damage to the player");
                }
                else
                {
                    Debug.LogWarning("Player component not found on object with Player tag");
                }
            }
        }

        // If no player was found in the hitObjects, log it
        if (!hitObjects.Any(obj => obj.CompareTag("Player")))
        {
            Debug.Log("No player found within explosion radius");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Override the base class TakeDamage method if you want to handle damage differently
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Add any Tomato-specific damage handling here
    }

    // Override MoveTowardsPlayer if you want to change how the Tomato moves
    protected override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
        // Add any Tomato-specific movement behavior here
    }
}
