using UnityEngine;

public class LoopingAudio : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public float loopStartTime = 0f;
    public float loopEndTime = 0f;

    private AudioSource audioSource;
    private float nextLoopTime;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        if (loopEndTime <= 0 || loopEndTime > backgroundMusic.length)
            loopEndTime = backgroundMusic.length;

        PlayMusic();
    }

    void Update()
    {
        if (audioSource.time >= nextLoopTime)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        audioSource.time = loopStartTime;
        audioSource.Play();
        nextLoopTime = loopEndTime;
    }
}
