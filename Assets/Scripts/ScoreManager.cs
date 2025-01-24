using UnityEngine;
using TMPro; // TextMeshPro
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText; // Change this line
    public Image scoreTitleImage;
    public Sprite eggFryerSprite;
    public Sprite veggieFanSprite;
    public Sprite broccolliHeadSprite;
    public Sprite zucchiniDestroyerSprite;
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
        scoreTitleImage.gameObject.SetActive(false);

        if (scoreText == null)
        {
            Debug.LogWarning("TextMeshProUGUI Score Text not assigned in the inspector!");
        }
        UpdateScoreDisplay();
    }

    public void AddScore(int points, Vector3 position)
    {
        scoreTitleImage.gameObject.SetActive(true);
        score += points;
        CreateScorePopup(points, position); // create popup for the score
        UpdateScoreTitle(score);
        UpdateScoreDisplay();

    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
    }

    private void UpdateScoreTitle(int currentScore)
    {
        if (scoreTitleImage != null)

        {

            if (score <= 100)
                scoreTitleImage.sprite = eggFryerSprite;
            else if (score <= 500)
                scoreTitleImage.sprite = veggieFanSprite;
            else if (score <= 1000)
                scoreTitleImage.sprite = broccolliHeadSprite;
            else
                scoreTitleImage.sprite = zucchiniDestroyerSprite;

        }
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
