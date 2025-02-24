using System.Collections;
using UnityEngine;

public class Whee : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;
    private string attackerName;

    public void SetTarget(Transform _target, string attacker)
    {
        target = _target;
        attackerName = attacker;
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
        Debug.Log("Cannon Ball hit target: " + target.name);

        int targetLayer = target.gameObject.layer;
        if (targetLayer == LayerMask.NameToLayer("Catapult"))
        {
            Launch catapult = target.GetComponent<Launch>();
            if (catapult != null)
            {
                catapult.TakeDamage(1, attackerName); // Apply damage
                BattleLogger.Instance.LogKill(gameObject.name, target.name);
                Debug.Log("Catapult took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("DefenseCatapult"))
        {
            DefenseLaunch catapult = target.GetComponent<DefenseLaunch>();
            if (catapult != null)
            {
                catapult.TakeDamage(1, attackerName); // Apply damage
                BattleLogger.Instance.LogKill(gameObject.name, target.name);
                Debug.Log("Defense Catapult took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("Archer"))
        {
            EnemyArcher archer = target.GetComponent<EnemyArcher>();
            if (archer != null)
            {
                archer.TakeDamage(1, attackerName); // Apply damage
                BattleLogger.Instance.LogKill(gameObject.name, target.name);
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else if (targetLayer == LayerMask.NameToLayer("DefenseArcher"))
        {
            ArcherTower darcher = target.GetComponent<ArcherTower>();
            if (darcher != null)
            {
                darcher.TakeDamage(1, attackerName); // Apply damage
                BattleLogger.Instance.LogKill(gameObject.name, target.name);
                Debug.Log("Enemy took damage");
            }
            else
            {
                Debug.Log("Never took damage");
            }
        }
        else
        {
            Debug.Log("Target is neither an Catapult nor an Archer");
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