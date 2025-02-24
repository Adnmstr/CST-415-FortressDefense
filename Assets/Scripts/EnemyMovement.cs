using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public int wallDamage = 1;
    public int playerDamage = 1;
    public float attackInterval = 1f; // Attack every 1 second

    private Rigidbody rb;
    private bool isAttacking = false;
    private GameObject currentTarget;
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isAttacking && currentTarget != null)
        {
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer >= attackInterval)
            {
                Attack(currentTarget);
                attackTimer = 0f; // Reset attack timer
            }
        }
        else
        {
            // Move forward when not attacking
            rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            isAttacking = true;
            currentTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            StopAttacking();
        }
    }

    void Attack(GameObject target)
    {
        if (target == null)
        {
            StopAttacking();
            return;
        }

        if (target.CompareTag("Player"))
        {
            Debug.Log("Enemy attacks the player!");
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerDamage, gameObject.name);
                if (playerHealth.health <= 0)
                {
                    StopAttacking(); // Resume moving after player is destroyed
                }
            }
        }
        else if (target.CompareTag("Wall"))
        {
            Debug.Log("Enemy attacks the wall!");
            WallHealth wallHealth = target.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(wallDamage);
                if (wallHealth.currentHealth <= 0)
                {
                    StopAttacking(); // Resume moving after wall is destroyed
                }
            }
        }
    }

    void StopAttacking()
    {
        isAttacking = false;
        currentTarget = null;
        attackTimer = 0f; // Reset attack timer
    }
}
