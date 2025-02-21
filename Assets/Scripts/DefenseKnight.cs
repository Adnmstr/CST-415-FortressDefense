using UnityEngine;

public class DefenseKnight : MonoBehaviour
{
    public float attackRange = 2f;      // Range within which knight can attack
    public int enemyDamage = 1;         // Damage dealt to enemies
    public float attackInterval = 1f;   // Attack every 1 second

    private bool isAttacking = false;
    private GameObject currentTarget;
    private float attackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && currentTarget != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                Attack(currentTarget);
                attackTimer = 0f; // Reset attack timer
            }
        }

    }
    void FixedUpdate()
    {
        // Check for enemies in range if not already attacking
        if (!isAttacking)
        {
            CheckForEnemies();
        }
    }

    void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Enemies"))
            {
                isAttacking = true;
                currentTarget = hit.gameObject;
                break;
            }
        }
    }

    void Attack(GameObject target)
    {
        if (target == null)
        {
            StopAttacking();
            return;
        }

        if (target.CompareTag("Enemies"))
        {
            Debug.Log("Knight defends against enemy!");
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(enemyDamage);
                if (enemyHealth.health <= 0)
                {
                    StopAttacking(); // Stop attacking if enemy is defeated
                }
            }
        }
    }

    void StopAttacking()
    {
        isAttacking = false;
        currentTarget = null;
        attackTimer = 0f;
    }

    // Optional: Visualize the attack range in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
