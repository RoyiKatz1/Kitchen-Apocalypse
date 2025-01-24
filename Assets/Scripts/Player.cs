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
    public Image healthFill;

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
        UpdateHealthBarColor();
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
        UpdateHealthBarColor();

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
        GameManager.Instance.EndGame();
        gameObject.SetActive(false);
    }

    void UpdateHealthBarColor()
    {
        float healthPercentage = health / maxHealth; // Calculate health percentage

        if (healthPercentage > 0.5f)
        {
            healthFill.color = Color.green; // Green when above 50%
        }
        else if (healthPercentage > 0.2f)
        {
            healthFill.color = Color.yellow; // Yellow between 20% and 50%
        }
        else
        {
            healthFill.color = Color.red; // Red below 20%
        }
    }

}
