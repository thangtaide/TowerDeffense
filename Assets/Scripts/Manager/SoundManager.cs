using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sound
    {
        BuildingDamaged,
        BuildingDestroyed,
        BuildingPlaced,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
        GameOver,
        Music
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private float soundVolume = 0.5f;
    private float musicVolume = 0.5f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.volume = musicVolume;
    }
    private void Start()
    {
        PlayMusic(Sound.Music);
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], soundVolume);
    }

    public void PlayMusic(Sound sound)
    {
        audioSource.clip = soundAudioClipDictionary[sound];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void IncreaseSoundVolume()
    {
        soundVolume += .1f;
        soundVolume = Mathf.Clamp01(soundVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
    public void DecreaseSoundVolume()
    {
        soundVolume -= .1f;
        soundVolume = Mathf.Clamp01(soundVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }

    public float GetSoundVolume()
    {
        return soundVolume;
    }

    public void IncreaseMusicVolume()
    {
        musicVolume += .1f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
    public void DecreaseMusicVolume()
    {
        musicVolume -= .1f;
        musicVolume = Mathf.Clamp01(musicVolume);
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
