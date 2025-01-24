using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float health = 100f;
    public float damageAmount = 5f;
    public float damageInterval = 1f;
    public int scoreValue = 10; // Default score value

    protected Transform player;
    protected float lastDamageTime;

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
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                Player playerScript = collision.gameObject.GetComponent<Player>();
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
