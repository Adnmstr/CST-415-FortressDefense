using UnityEngine;

public class Fortress : MonoBehaviour
{
    int health = 3;
    bool test = false;
    public PositionType type;

    private void Start()
    {
        health = 3;
        if (test) { ReceiveDamage(test); }
    }

    void ReceiveDamage(bool hit)
    {
        if (hit && health > 0)
        {
            health -= 1;
        }
        else { return; }
    }
}