using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] SoundManager soundManager;
    private Transform musicTransform, soundTransform;
    private TextMeshProUGUI musicVolumeText, soundVolumeText;

    private void Awake()
    {
        Instance = this;
        musicTransform = transform.Find("Music").transform;
        soundTransform = transform.Find("Sound").transform;

        soundVolumeText = soundTransform.Find("SoundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = musicTransform.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();

        musicTransform.Find("MusicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.IncreaseMusicVolume();
            UpdateText();
        });
        musicTransform.Find("MusicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.DecreaseMusicVolume();
            UpdateText();
        });
        soundTransform.Find("SoundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.IncreaseSoundVolume();
            UpdateText();
        });
        soundTransform.Find("SoundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.DecreaseSoundVolume();
            UpdateText();
        });

        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenu);
        });
    }

    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleVisible();
        }
    }
    private void UpdateText()
    {
        soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetSoundVolume() * 10).ToString());
        musicVolumeText.SetText(Mathf.RoundToInt(soundManager.GetMusicVolume() * 10).ToString());
    }

    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }
}
