// Attach this to enemy prefabs and assign a target position (e.g., gates or towers).

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public Transform targetPosition; // Assign in Unity Inspector

    void Update()
    {
        if (targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        }
    }
}
