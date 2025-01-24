using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float health = 100f;
    public float maxHealth = 100f;
    public float moveSpeed = 5f;

    private Vector2 movement;
    private bool isDead = false;
    public Slider healthBar;

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

    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
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
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.value = health;
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
