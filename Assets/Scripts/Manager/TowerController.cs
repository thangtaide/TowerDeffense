using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private float targetMaxRadius = 10f;
    [SerializeField] private float lookForTargetTimerMax = 0.2f;
    [SerializeField] private float shootTimerMax = 0.3f;
    private float lookForTargetTimer;
    private float shootTimer;
    private EnemyController enemyTarget;
    private Vector3 projectSpawnPosition;

    private void Awake()
    {
        projectSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
    }

    private void Start()
    {
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
        shootTimer = Random.Range(0f, shootTimerMax);
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0f)
        {
            shootTimer += shootTimerMax;
            if(enemyTarget != null)
            {
                ArrowProjectileController.CreateArrow(projectSpawnPosition, enemyTarget);
            }
        }
    }

    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if(lookForTargetTimer < 0f )
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }

    private void LookForTarget()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach(Collider2D collider in colliderArray)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            if(enemy != null)
            {
                if(enemyTarget == null) { enemyTarget = enemy; }
                else
                {
                    float distanceCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    float distanceTarget = Vector3.Distance(transform.position,enemyTarget.transform.position);
                    if(distanceCurrentEnemy < distanceTarget) {
                        enemyTarget = enemy;
                    }
                }
            }
        }
    }
}
