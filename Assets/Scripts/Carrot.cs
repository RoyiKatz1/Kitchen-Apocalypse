using UnityEngine;

public class Carrot : Enemy
{

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.enemyDeathSound);
        animator.SetTrigger("Die");

        Debug.Log($"{GetType().Name} died");
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue, transform.position);
        }
        else
        {
            Debug.LogWarning("ScoreManager instance not found. Score not added.");
        }
        Destroy(gameObject, 0.1f);
    }


}