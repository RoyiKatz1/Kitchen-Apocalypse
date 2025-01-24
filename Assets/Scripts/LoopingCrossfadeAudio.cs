using UnityEngine;
using System.Collections;

public class LoopingAudio : MonoBehaviour
{
    public float loopStartTime = 0f;
    public float loopEndTime = 0f;
    public float crossfadeDuration = 1f;

    private AudioSource audioSource;
    private float nextLoopTime;

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

        audioSource.loop = false;

        if (loopEndTime <= 0 || loopEndTime > audioSource.clip.length)
            loopEndTime = audioSource.clip.length;

        PlayMusic();
    }

    void Update()
    {
        if (audioSource.isPlaying && audioSource.time >= nextLoopTime - crossfadeDuration)
        {
            StartCoroutine(CrossfadeLoop());
        }
    }

    void PlayMusic()
    {
        audioSource.time = loopStartTime;
        audioSource.Play();
        nextLoopTime = loopEndTime;
    }

    IEnumerator CrossfadeLoop()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = audioSource.clip;
        newSource.time = loopStartTime;
        newSource.Play();

        float t = 0;
        while (t < crossfadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0.2f, 0, t / crossfadeDuration);
            newSource.volume = Mathf.Lerp(0, 0.2f, t / crossfadeDuration);
            yield return null;
        }

        Destroy(audioSource);
        audioSource = newSource;
        nextLoopTime = loopEndTime;
    }
}
