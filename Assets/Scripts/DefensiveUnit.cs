// This script defines different unit types.

using UnityEngine;

[System.Serializable]
public class DefensiveUnit
{
    public string unitName;
    public UnitType type;
    public bool isAvailable = true; // Track availability

    public DefensiveUnit(string name, UnitType type)
    {
        unitName = name;
        this.type = type;
    }
}

public enum UnitType
{
    Archer,  // Can be placed on Walls and Towers
    Catapult, // Can be placed on Towers and Gates
    Knight // Can be placed on Gates
}