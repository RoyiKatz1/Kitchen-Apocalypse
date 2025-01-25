using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.2f;
    public float attackCooldown = 0f;
    public int attackDamage = 100;
    public LayerMask enemyLayer;
    public float attackAngle = 110f;

    private int baseDamage;
    private int currentDamageIncrease = 0;
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
        baseDamage = attackDamage;
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
        AudioManager.Instance.PlaySound(AudioManager.Instance.playerAttackSound);

        lastAttackTime = Time.time;

        Vector2 attackDirection = playerMovement.IsFacingRight ? Vector2.right : Vector2.left;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D collider in hitColliders)
        {
            Vector2 directionToTarget = ((Vector2)collider.transform.position - (Vector2)transform.position).normalized;
            float angle = Vector2.Angle(attackDirection, directionToTarget);

            if (angle <= attackAngle / 2)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                    Debug.Log($"Dealt {attackDamage} damage to enemy: {collider.gameObject.name}");
                }
            }
        }

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

    public void ActivatePowerUp(int damageIncrease)
    {
        currentDamageIncrease += damageIncrease;
        attackDamage = baseDamage + currentDamageIncrease;
        Debug.Log($"Power-up collected! Damage increased to {attackDamage}.");
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

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Vector3 startRay = Quaternion.Euler(0, 0, attackAngle / 2) * attackDirection * attackRange;
            Vector3 endRay = Quaternion.Euler(0, 0, -attackAngle / 2) * attackDirection * attackRange;

            Gizmos.DrawLine(transform.position, transform.position + startRay);
            Gizmos.DrawLine(transform.position, transform.position + endRay);

            int segments = 20;
            Vector3 previousPoint = transform.position + startRay;
            for (int i = 1; i <= segments; i++)
            {
                float t = i / (float)segments;
                float angle = Mathf.Lerp(attackAngle / 2, -attackAngle / 2, t);
                Vector3 currentPoint = transform.position + Quaternion.Euler(0, 0, angle) * attackDirection * attackRange;

                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }
    }
}
