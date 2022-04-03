using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class audioManager : MonoBehaviour
{
    public AudioClip impact;
    public AudioClip[] moveSounds;
    public AudioClip[] closeSounds;
    public AudioClip[] laugh;
    public AudioClip music;
    public AudioClip startGameSound;
    public AudioClip nextAreaSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void playImpact()
    {
        audioSource.PlayOneShot(impact, .7f);
    }
    public void playClose()
    {
        audioSource.PlayOneShot(closeSounds[Random.Range(0, closeSounds.Length)], .5f);
    }
    public void playLaugh()
    {
        audioSource.PlayOneShot(laugh[Random.Range(0, laugh.Length)], .5f);
    }
    public void playMove()
    {
        audioSource.PlayOneShot(moveSounds[Random.Range(0, moveSounds.Length)],.5f);
    }
    public void playStartSound()
    {

        audioSource.PlayOneShot(startGameSound,.5f);
    }
    public void playNextAreaSound()
    {

        audioSource.PlayOneShot(nextAreaSound, .5f);
    }
}