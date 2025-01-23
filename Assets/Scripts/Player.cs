using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float health = 100f;
    public float moveSpeed = 5f;

    private Vector2 movement;
    private bool isDead = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(moveHorizontal, moveVertical).normalized;
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Player health: " + health);
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");
        Time.timeScale = 0; // This will pause the game
    }
}
