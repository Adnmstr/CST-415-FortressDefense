using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyArcher : MonoBehaviour
{
    public int health = 1;
    public float speed = 2f;
    public float attackRange = 5f;
    public float fireRate = 1f;
    public LayerMask defenseArcher;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public LayerMask defenseKnights;


    private float nextFireTime = 1f;
    private bool isMoving = true;
    private Transform target;

    void Update()
    {
        Move();
        FindTarget();
        if (target != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 5f / fireRate;
        }
    }

    void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, defenseArcher | defenseKnights);
        Debug.Log("Defense in range: " + enemies.Length);

        if (enemies.Length > 0)
        {
            isMoving = false;
            target = enemies[0].transform; // Target the first enemy found
            Debug.Log("Target Found: " + target.name);
        }
        else
        {
            target = null;
            Debug.Log("No target found");
        }
    }

    void Move()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("took damage");
        if (health <= 0)
        {
            Die();
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
            arrowScript.SetTarget(target);
        }
        else
        {
            Debug.Log("no script");
        }
    }

    void Die()
    {
        Debug.Log(gameObject + "died");
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
