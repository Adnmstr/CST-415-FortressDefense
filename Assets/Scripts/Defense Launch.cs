using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenseLaunch : MonoBehaviour
{
    public float attackRange = 500f;
    public int health = 1;
    private Transform target;
    public LayerMask catapult;
    public LayerMask archer;
    private string lastAttacker = "Unknown";
    private float nextFireTime = 1f;
    public float fireRate = 1f;
    public GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(0, -90, -30);
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if (target != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 5f / fireRate;
        }
    }

    private void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, catapult | archer);
        Debug.Log("Defense in range: " + enemies.Length);

        if (enemies.Length > 0)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            target = enemies[randomIndex].transform; // Target the first enemy found
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

        GameObject cannonball = Instantiate(projectile, transform.position, Quaternion.LookRotation(target.position - transform.position));
        Whee projScript = cannonball.GetComponent<Whee>();

        if (projScript != null)
        {
            projScript.SetTarget(target, gameObject.name);
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
}
