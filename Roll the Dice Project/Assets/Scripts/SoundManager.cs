using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundPlayerPrefab;

    private GameObject musicPlayer;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip deadMusic;

    [SerializeField] AudioClip tileRevealGood;
    [SerializeField] AudioClip tileRevealBad;

    public void TileRevealGood() { PlaySound(tileRevealGood, 1f); }

    public void TileRevealBad() { PlaySound(tileRevealBad, 1f); }

    public void PlayMenuMusic() { PlayMusic(menuMusic); }
    public void PlayGameMusic() { PlayMusic(gameMusic); }
    public void PlayDeadMusic() { PlaySound(deadMusic, 0.05f); }

    private void PlaySound(AudioClip clip, float vol)
    {
        GameObject soundPlayer = Instantiate(soundPlayerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = vol; // soundVolume;
        audioSource.Play();
        Destroy(soundPlayer, audioSource.clip.length + 0.1f);
    }

    private void PlayMusic(AudioClip clip)
    {
        musicPlayer = Instantiate(soundPlayerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = musicPlayer.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.05f; // musicVolume;
        audioSource.Play();
    }
}