using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float health = 100f;
    public float damageAmount = 10f;
    public float damageInterval = 1f;

    private Transform player;
    private float lastDamageTime;

    void Update()
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

    void FindPlayer()
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

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
