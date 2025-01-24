using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    private float elapsedTime = 0f;

    public float healthMultiplier = 0.1f;
    public float speedMultiplier = 0.05f;
    public float damageMultiplier = 0.05f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public float GetMultiplier(float baseMultiplier)
    {
        return 1f + (elapsedTime / 60f) * baseMultiplier;
    }
}
