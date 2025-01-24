using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float baseMoveSpeed = 2f;
    public float baseHealth = 100f;
    public float baseDamageAmount = 5f;
    public float damageInterval = 1f;
    public int scoreValue = 10;

    protected float moveSpeed;
    protected float health;
    protected float damageAmount;
    protected Transform player;
    protected float lastDamageTime;

    protected virtual void Start()
    {
        UpdateStats();
        FindPlayer();
    }

    public virtual void UpdateStats()
    {
        if (DifficultyManager.Instance != null)
        {
            moveSpeed = baseMoveSpeed * DifficultyManager.Instance.GetMultiplier(DifficultyManager.Instance.speedMultiplier);
            health = baseHealth * DifficultyManager.Instance.GetMultiplier(DifficultyManager.Instance.healthMultiplier);
            damageAmount = baseDamageAmount * DifficultyManager.Instance.GetMultiplier(DifficultyManager.Instance.damageMultiplier);
        }
        else
        {
            moveSpeed = baseMoveSpeed;
            health = baseHealth;
            damageAmount = baseDamageAmount;
        }
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    protected void FindPlayer()
    {
        if (Player.Instance != null)
        {
            player = Player.Instance.transform;
        }
        else
        {
            Debug.Log("Player instance not found. Make sure the Player script is attached to a GameObject in the scene.");
        }
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        FlipSprite(-direction.x);

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    protected void FlipSprite(float directionX)
    {
        if ((directionX > 0 && transform.localScale.x < 0) || (directionX < 0 && transform.localScale.x > 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                Player playerScript = collision.gameObject.GetComponent<Player>();
                AudioManager.Instance.PlaySound(AudioManager.Instance.enemyAttackSound);
                playerScript.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{GetType().Name} took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.enemyDeathSound);

        Debug.Log($"{GetType().Name} died");
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
}
