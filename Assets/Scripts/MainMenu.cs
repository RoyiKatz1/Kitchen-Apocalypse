using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private BackgroundMusic backgroundMusic;

    private void Start()
    {
        backgroundMusic = FindAnyObjectByType<BackgroundMusic>();

        if (backgroundMusic != null)
        {
            backgroundMusic.PlayMusic();
        }
    }

    public void PlaySinglePlayer()
    {
        backgroundMusic.PlayMusic();
        // Load your single-player scene
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayCoop()
    {
        backgroundMusic.PlayMusic();
        // Load your co-op scene
    }

    public void ExitGame()
    {
        // Exit the game (works only in a built application)
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
