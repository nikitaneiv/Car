using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private Car car;

    private void Start()
    {
        car = FindObjectOfType<Car>();
    }

    private void Update()
    {
        if (car.SoundHasBeenPlayed && !audioSource.isPlaying)
        {
            PlaySound();
        }
        else if (!car.SoundHasBeenPlayed && audioSource.isPlaying)
        {
            StopSound();
        }
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void StopSound()
    {
        audioSource.Stop();
    }
}
