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
            BecomeInvisibleAndBreak();
        }
    }

    void BecomeInvisibleAndBreak()
    {
        // Make the wall invisible
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Make all child renderers invisible
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.enabled = false;
        }

        Debug.Log("Wall has been destroyed!");
        Destroy(gameObject);
    }
}