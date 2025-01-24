using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;
    private AudioSource audioSource;
    public float pauseDuration = 3f;  // Duration of pause between plays
    private Coroutine playRoutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = false;  // Disable built-in looping
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && playRoutine == null)
        {
            playRoutine = StartCoroutine(PlayMusicWithPause());
        }
    }

    private IEnumerator PlayMusicWithPause()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            if (playRoutine != null)
            {
                StopCoroutine(playRoutine);
                playRoutine = null;
            }
            audioSource.Stop();
        }
    }
}
