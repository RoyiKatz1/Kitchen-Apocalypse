using UnityEngine;
using System.Collections.Generic;

public class CirclingItem : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float orbitSpeed = 50f; // Speed of orbit (degrees per second)
    public float orbitRadius = 1.2f; // Distance from the player
    public int damageAmount = 100; // Damage dealt to enemies
    public float damageCooldown = 1f; // Time between damage applications to the same enemy

    private float angle; // Current angle of rotation (in degrees)
    private Dictionary<Collider2D, float> lastDamageTime = new Dictionary<Collider2D, float>(); // Tracks when each enemy was last damaged
    private bool isCircling = false; // Whether the item is currently circling

    public void StartCircling()
    {
        isCircling = true;
        angle = 0f; // Start from an initial angle
    }

    void Update()
    {
        if (player == null || !isCircling) return;

        // Increment the angle based on orbitSpeed (convert speed to radians per second)
        angle += orbitSpeed * Time.deltaTime;

        // Keep angle within 0-360 degrees for readability (optional)
        if (angle >= 360f) angle -= 360f;

        // Calculate the new position of the circling item
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * orbitRadius;
        transform.position = player.position + offset;

        // Optional: Rotate the item to face outward from the player (can be removed if not needed)
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float rotationAngle = Mathf.Atan2(-directionToPlayer.y, -directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isCircling) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (!lastDamageTime.ContainsKey(other) || Time.time - lastDamageTime[other] >= damageCooldown)
            {
                enemy.TakeDamage(damageAmount);
                Debug.Log($"Circling item dealt {damageAmount} damage to {other.name}");
                lastDamageTime[other] = Time.time;
            }
        }
    }
}
