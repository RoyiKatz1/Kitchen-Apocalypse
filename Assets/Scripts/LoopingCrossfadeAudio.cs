using UnityEngine;

public class SimpleLoopingAudio : MonoBehaviour
{
    public float loopStartTime = 0f;
    public float loopEndTime = 0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        if (audioSource.clip == null)
        {
            Debug.LogError("Audio clip is not assigned to the AudioSource!");
            return;
        }

        // Set up the audio source
        audioSource.loop = false; // We'll handle looping manually
        audioSource.volume = 0.2f;

        // Adjust loop end time if it's invalid
        if (loopEndTime <= 0 || loopEndTime > audioSource.clip.length)
            loopEndTime = audioSource.clip.length;

        PlayMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying || audioSource.time >= loopEndTime)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        audioSource.time = loopStartTime;
        audioSource.Play();
        Debug.Log("Looping audio at time: " + Time.time);
    }
}
