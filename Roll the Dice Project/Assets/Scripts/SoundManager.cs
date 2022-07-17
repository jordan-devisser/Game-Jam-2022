using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundPlayerPrefab;

    private GameObject musicPlayer;

    [SerializeField] float musicVolume = 0.5f;
    [SerializeField] float soundVolume = 0.5f;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip deadMusic;

    [SerializeField] AudioClip tileRevealGood;
    [SerializeField] AudioClip tileRevealBad;

    public void TileRevealGood() { PlaySound(tileRevealGood); }

    public void TileRevealBad() { PlaySound(tileRevealBad); }

    public void PlayMenuMusic() { PlayMusic(menuMusic); }
    public void PlayGameMusic() { PlayMusic(gameMusic); }
    public void PlayDeadMusic() { PlayMusic(deadMusic); }

    private void PlaySound(AudioClip clip)
    {
        GameObject soundPlayer = Instantiate(soundPlayerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = 1f; // soundVolume;
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