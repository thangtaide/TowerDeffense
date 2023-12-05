using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{

    [SerializeField] private EnemyWaveManager waveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform waveSpawnPositionIndicator;
    private RectTransform enemyClosestPositionIndicator;
    private FillerTargetController fillerTarget;

    private void Awake()
    {
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveSpawnPositionIndicator = transform.Find("WaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("ClosestPositionIndicator").GetComponent<RectTransform>();
        fillerTarget = GetComponent<FillerTargetController>();
    }

    private void Start()
    {
        waveManager.OnWaveNumberChange += WaveManager_OnWaveNumberChange;
        SetWaveNumberText("Wave " + waveManager.GetWaveNumber());
    }

    private void WaveManager_OnWaveNumberChange(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave "+waveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();

    }
    
    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = waveManager.GetWaveSpawnTimer();
        if (nextWaveSpawnTimer > 0)
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
        else
        {
            SetMessageText("");
        }

    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 DirToNextSpawnPosition = (waveManager.GetSpawnPosition() - Camera.main.transform.position).normalized;
        DirToNextSpawnPosition.z = 0;

        waveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(DirToNextSpawnPosition));
        float distanceToNextSpawnPosition = Vector3.Distance(waveManager.GetSpawnPosition(), Camera.main.transform.position);
        waveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > Camera.main.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        Transform targetEnemy = TargetController.GetTarget(fillerTarget);

        if (targetEnemy != null)
        {
            Vector3 DirToEnemyClosestPosition = (targetEnemy.position - Camera.main.transform.position).normalized;
            DirToEnemyClosestPosition.z = 0;

            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(DirToEnemyClosestPosition));
            float distanceToNextEnemyClosestPosition = Vector3.Distance(targetEnemy.position, Camera.main.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToNextEnemyClosestPosition > Camera.main.orthographicSize * 1.5f);
        }
        else
        {

            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string text)
    {
        waveMessageText.text = text;
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.text = text;
    }
    
}
