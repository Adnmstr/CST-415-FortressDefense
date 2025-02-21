using System.Collections;
using UnityEngine;

public class Whee : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3((1.0f / 3.0f), (10.0f / 3.0f), (10.0f / 3.0f));
        transform.Rotate(0, 90, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Move the arrow towards the target in 3D space
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, target.position) < 0.1f) // Adjust the threshold as needed
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        Debug.Log("Arrow hit target: " + target.name);

        int targetLayer = target.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Archer"))
        {
            EnemyArcher enemy = target.GetComponent<EnemyArcher>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Apply damage
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("DefenseArcher"))
        {
            ArcherTower archer = target.GetComponent<ArcherTower>();
            if (archer != null)
            {
                archer.TakeDamage(1); // Apply damage 
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("Knights"))
        {
            PlayerHealth knight = target.GetComponent<PlayerHealth>();
            if (knight != null)
            {
                knight.TakeDamage(1); // Apply damage 
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("DefenseKnight"))
        {
            PlayerHealth dknight = target.GetComponent<PlayerHealth>();
            if (dknight != null)
            {
                dknight.TakeDamage(1); // Apply damage 
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else
        {
            Debug.Log("Target is neither an Enemy nor an Archer");
        }

        Destroy(gameObject); // Destroy the arrow once it hits the target
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger the hit if the arrow collides with the target
        if (other.transform == target)
        {
            HitTarget();
        }
    }
}