using LTA.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToForNextWave,
        SpawningWave
    }

    public event EventHandler OnWaveNumberChange;

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextSpawnPositionTransform;
    [SerializeField] private float nextWaveSpawnTimerMax = 10f;
    [SerializeField] private int remainingEnemySpawnAmountStart;

    private int remainingEnemySpawnAmount;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private Vector3 spawnPosition;
    private State state;
    int waveNumber;

    private void Start()
    {
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextSpawnPositionTransform.position = spawnPosition;
        waveNumber = 0;
        state = State.WaitingToForNextWave;
        nextWaveSpawnTimer = nextWaveSpawnTimerMax;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToForNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        EnemyController.CreateEnemy(spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if(remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToForNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = nextWaveSpawnTimerMax;
                        }
                    }
                }
                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = remainingEnemySpawnAmountStart + waveNumber * 2;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChange?.Invoke(this, EventArgs.Empty);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyWaveStarting);
    }
    
    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
public class EnemyWaveInstance: SingletonMonoBehaviour<EnemyWaveManager> { }
