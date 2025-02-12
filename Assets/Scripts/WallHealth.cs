using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Wall took " + amount + " damage. Health left: " + currentHealth);

        if (currentHealth <= 0)
        {
            BreakWall();
        }
    }

    void BreakWall()
    {
        Debug.Log("Wall has been destroyed!");
        Destroy(gameObject);
    }
}
