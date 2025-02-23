using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    public GameObject endGameScreen; // End game screen panel
    public GameObject regularScoreText;
    public bool isGameOver = false; // Variable to track game state

    private void Awake()
    {
        // Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager
        }
    }

    private void Start()
    {
        if (endGameScreen != null)
        {
            endGameScreen.SetActive(false); // Ensure the end game screen is disabled at the start
        }
    }


    public void EndGame()
    {
        if (isGameOver) return; // If the game is already over, do not perform any action

        isGameOver = true;
        Debug.Log("Game Over!");

        // Stop the game (optional)
        Time.timeScale = 0;

        // הסתר את הניקוד הרגיל
        if (regularScoreText != null)
        {
            regularScoreText.SetActive(false);
        }

        // Show the end game screen
        if (endGameScreen != null)
        {
            endGameScreen.SetActive(true);

            // Update the score text on the end game screen
            TextMeshProUGUI scoreText = endGameScreen.GetComponentInChildren<TextMeshProUGUI>();
            if (scoreText != null)
            {
                int finalScore = ScoreManager.Instance.GetScore(); // Get the final score from ScoreManager

                scoreText.text = $"{finalScore}";
                Debug.Log("Final Score Displayed: " + finalScore); // Debug for validation
            }
            else
            {
                Debug.LogError("Score Text not found in EndGamePanel!");
            }
        }
    }

    public void CleanupScene()
    {
        var persistentObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None)
            .Where(go => go.scene.name == "DontDestroyOnLoad")
            .ToArray();

        foreach (var obj in persistentObjects)
        {
            Destroy(obj);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        CleanupScene();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


}
