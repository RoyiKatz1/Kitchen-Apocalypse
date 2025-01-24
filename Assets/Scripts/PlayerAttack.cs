using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 100;
    public LayerMask enemyLayer;
    public GameObject attackEffectPrefab;

    private Animator animator;
    private float lastAttackTime;
    private PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on this GameObject!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        // Determine attack direction
        Vector2 attackDirection = playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;

        // Debug draw the attack area
        Debug.DrawLine(transform.position, attackPosition, Color.red, 1f);

        // Get all colliders in the scene
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, 10f); // Large radius to ensure we catch everything nearby

        Debug.Log($"Total colliders found: {allColliders.Length}");

        foreach (Collider2D collider in allColliders)
        {
            Debug.Log($"Collider: {collider.gameObject.name}, Layer: {LayerMask.LayerToName(collider.gameObject.layer)}, Position: {collider.transform.position}");

            if (((1 << collider.gameObject.layer) & enemyLayer) != 0)
            {
                float distance = Vector2.Distance(attackPosition, collider.transform.position);
                Debug.Log($"Enemy found: {collider.gameObject.name}, Distance: {distance}");

                if (distance <= attackRange / 2)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(attackDamage);
                        Debug.Log($"Dealt {attackDamage} damage to enemy: {collider.gameObject.name}");
                    }
                    else
                    {
                        Debug.LogWarning($"Enemy component not found on GameObject: {collider.gameObject.name}");
                    }
                }
            }
        }

        // Spawn attack effect
        if (attackEffectPrefab != null)
        {
            Quaternion effectRotation = Quaternion.Euler(0, 0, attackDirection == Vector2.right ? 0 : 180);
            GameObject effect = Instantiate(attackEffectPrefab, attackPosition, effectRotation);
            Debug.Log($"Attack effect spawned at {attackPosition}");
        }
        else
        {
            Debug.LogWarning("Attack effect prefab is not assigned!");
        }

        // Trigger animation
        if (animator != null && HasParameter("Attack"))
        {
            animator.SetTrigger("Attack");
            Debug.Log("Attack animation triggered");
        }
        else
        {
            Debug.LogWarning("Animator or 'Attack' parameter not set up correctly!");
        }
    }

    private bool HasParameter(string paramName)
    {
        if (animator == null)
            return false;

        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (playerMovement != null)
        {
            Vector2 attackDirection = playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
            Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition, attackRange / 2);
        }
    }
}
