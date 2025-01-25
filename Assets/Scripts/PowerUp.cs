using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int damageIncrease = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.ActivatePowerUp(damageIncrease);
                Destroy(gameObject);
            }
        }
    }
}
