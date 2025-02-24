using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    public int health = 1;
    public float attackRange = 5f;
    public float fireRate = 1f;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public LayerMask enemyLayer;
    public LayerMask enemy2Layer;

    private string lastAttacker = "Unknown";
    private float nextFireTime = 1f;
    private Transform target;

    void Update()
    {
        FindTarget();
        if (target != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 5f / fireRate;
        }
    }

    void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer | enemy2Layer);
        Debug.Log("Enemies in range: " + enemies.Length);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform; // Target the first enemy found
            Debug.Log("Target Found: " + target.name);
        }
        else
        {
            target = null;
            Debug.Log("No target found");
        }
    }

    void Shoot()
    {
        if (target == null) return;
        Debug.Log("Shooting at: " + target.name);

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.LookRotation(target.position - firePoint.position));
        Arrow arrowScript = arrow.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            arrowScript.SetTarget(target, gameObject.name);
        }
        else
        {
            Debug.Log("no script");
        }
    }

    public void TakeDamage(int damage, string attacker)
    {
        lastAttacker = attacker;

        health -= damage;
        Debug.Log(gameObject.name + " took damage from " + attacker);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");

        if (BattleLogger.Instance != null)
        {
            BattleLogger.Instance.LogKill(lastAttacker, gameObject.name);
        }
        else
        {
            Debug.LogWarning("BattleLogger not found!");
        }

        Destroy(gameObject);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}