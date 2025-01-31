using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Move forward in local space
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}