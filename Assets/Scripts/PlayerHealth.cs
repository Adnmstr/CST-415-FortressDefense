using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1; // One hit = death
    private string lastAttacker = "Unknown";

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
