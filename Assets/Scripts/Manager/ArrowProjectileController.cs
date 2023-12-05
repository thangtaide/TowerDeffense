using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileController : MonoBehaviour
{
    [SerializeField] float timeToDie = 2f;
    [SerializeField] int damageAmount = 10;
    Vector3 moveDir;
    public static ArrowProjectileController CreateArrow(Vector3 position, EnemyController enemy)
    {
        Transform arrowPrefab = Resources.Load<Transform>("ArrowProjectile");
        Transform arrowTransform = Instantiate(arrowPrefab, position, Quaternion.identity);
        ArrowProjectileController arrowProjectile = arrowTransform.GetComponent<ArrowProjectileController>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;
    }

    private void Update()
    {
        HandleMove();
        HandleRotate();
        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            Destroy(gameObject);
        }
    }



    [SerializeField] float moveSpeed = 20f;
    private EnemyController enemyTarget;

    private void SetTarget(EnemyController enemyTarget)
    {
        this.enemyTarget = enemyTarget;
    }

    private void HandleRotate()
    {
        if (enemyTarget != null)
        {
            Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;

            transform.eulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVector(direction));
        }
    }
    private void HandleMove()
    {
        if (enemyTarget != null)
        {
            moveDir = (enemyTarget.transform.position - transform.position).normalized;
        }

        transform.position += moveSpeed * moveDir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
