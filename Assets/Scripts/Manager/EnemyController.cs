using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController CreateEnemy(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("Enemy");
        Transform enemyTransform = Instantiate(enemyPrefab, position, Quaternion.identity);
        EnemyController enemyController = enemyTransform.GetComponent<EnemyController>();
        return enemyController;
    }

    [SerializeField] float lookForTargetTimerMax = 0.2f;
    [SerializeField] private float moveSpeed = 6f;

    private HealthSystem healthSystem;
    private float lookForTargetTimer=0f;
    private FillerTargetController fillerTarget;
    private Rigidbody2D rigidbody2D;
    private Transform targetTransform;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        fillerTarget = GetComponent<FillerTargetController>();
        lookForTargetTimer = Random.Range(0f,lookForTargetTimerMax);
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
    }

    private void HealthSystem_OnTakeDamage(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"),transform.position, Quaternion.identity);
        CamerachineShake.Instance.SharkCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(.5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    void HandleMovement()
    {
        if (targetTransform != null)
        {
            MoveToTarget(targetTransform.position);
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }
    }

    void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            targetTransform = TargetController.GetTarget(fillerTarget);
        }
    }

    void MoveToTarget(Vector3 targetPosition)
    {
        Vector3 moveDir = targetPosition - transform.position;
        Move(moveDir);
    }

    void Move(Vector3 moveDir)
    {
        rigidbody2D.velocity = moveDir.normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem health = building.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(10);
                this.healthSystem.TakeDamage(999);
            }
        }
    }
}
