using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Launch : MonoBehaviour
{
    public float attackRange = 500f;
    private bool isMoving = true;
    private Transform target;
    public LayerMask defenseArcher;
    public LayerMask defenseKnights;
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

    void Shoot()
    {
        if (target == null) return;
        Debug.Log("Shooting at: " + target.name);

        GameObject cannonball = Instantiate(projectile, transform.position, Quaternion.LookRotation(target.position - transform.position));
        Whee projScript = cannonball.GetComponent<Whee>();

        if (projScript != null)
        {
            projScript.SetTarget(target);
        }
        else
        {
            Debug.Log("no script");
        }
    }
}
