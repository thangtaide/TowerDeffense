using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        transform.Find("RetryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenu);
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("WavesSurviveText").GetComponent<TextMeshProUGUI>().
            SetText("You Survived " + EnemyWaveInstance.Instance.GetWaveNumber() + " Waves!" );
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
