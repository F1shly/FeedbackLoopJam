using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fullLength, loopClip;
    public float fadeDuration = 2f; // Seconds to fade in
    private bool isLoopingStarted = false;

    public void PlayMusic()
    {
        if (!isLoopingStarted)
        {
            isLoopingStarted = true;
            StartCoroutine(PlayIntroThenLoopWithFade());
        }
    }

    private IEnumerator PlayIntroThenLoopWithFade()
    {
        // Fade in intro clip
        audioSource.clip = fullLength;
        audioSource.loop = false;
        audioSource.volume = 0f;
        audioSource.Play();

        yield return StartCoroutine(FadeIn(audioSource, fadeDuration));

        // Wait for the intro clip to finish
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Switch to loop clip
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.volume = 0.01f;
        audioSource.Play();

        //yield return StartCoroutine(FadeIn(audioSource, fadeDuration));
    }

    private IEnumerator FadeIn(AudioSource source, float duration)
    {
        float startTime = Time.time;
        while (source.volume < 0.01f)
        {
            source.volume = Mathf.Clamp01((Time.time - startTime) / duration) * 0.01f;
            yield return null;
        }
        source.volume = 0.01f;
    }
}
