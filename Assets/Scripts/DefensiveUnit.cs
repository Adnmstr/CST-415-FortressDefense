using UnityEngine;

[System.Serializable]
public class DefensiveUnit
{
    [Tooltip("Name for identification (e.g., 'Archer A').")]
    public string unitName;

    [Tooltip("Type of unit (Archer, Catapult, or Knight).")]
    public UnitType type;

    [HideInInspector]
    public bool isAvailable = true;

    [Tooltip("Prefab used to represent this defensive unit in the scene.")]
    public GameObject unitPrefab;

    public DefensiveUnit(string name, UnitType type)
    {
        this.unitName = name;
        this.type = type;
    }
}

public enum UnitType
{
    Archer,    // For walls (and towers)
    Catapult,  // For towers (and gates)
    Knight     // For gates
}
