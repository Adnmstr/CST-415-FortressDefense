using UnityEngine;

public class Fortress : MonoBehaviour
{
    public int health = 3;
    public bool test = false;
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

/*public enum PositionType
{
    Wall,
    Tower,
    Gate
}
*/