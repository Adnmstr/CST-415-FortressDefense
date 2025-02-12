using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1; // One hit = death

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player took damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject);
    }
}
