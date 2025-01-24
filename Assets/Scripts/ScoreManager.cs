using UnityEngine;
using TMPro; // TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText; // Change this line
    public GameObject popupPrefab; // prefab for the popup score
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogWarning("TextMeshProUGUI Score Text not assigned in the inspector!");
        }
        UpdateScoreDisplay();
    }

    public void AddScore(int points, Vector3 position)
    {
        score += points;
        CreateScorePopup(points, position); // create popup for the score
        UpdateScoreDisplay();

    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            string title = GetScoreTitle(score); // get the title based on the score
            scoreText.text = $"{title}: {score}";
        }
    }

    private string GetScoreTitle(int currentScore)
    {
        if (currentScore <= 100) return "מטגן ביצים";
        if (currentScore <= 500) return "חובב ירקות";
        if (currentScore <= 1000) return "מלטף עגבניות";
        return "משמיד קישואים";
    }

    private void CreateScorePopup(int points, Vector3 position)
    {
        if (popupPrefab != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

            GameObject popup = Instantiate(popupPrefab, screenPosition, Quaternion.identity, scoreText.canvas.transform);
            TextMeshProUGUI textMesh = popup.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = $"+{points}";
            }

            // add a little animation to the popup
            Destroy(popup, 1.0f);
        }
        else
        {
            Debug.LogWarning("Popup Prefab not assigned in the inspector!");
        }
    }


}
