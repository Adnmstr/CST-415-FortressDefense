// This script defines valid placements for each position.

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefensivePosition
{
    public string positionName;
    public PositionType type;
    public List<UnitType> allowedUnits; // Constraints on which units can be assigned
    public DefensiveUnit assignedUnit; // Holds the assigned unit
    public Transform positionTransform;

    public DefensivePosition(string name, PositionType type, List<UnitType> allowedUnits, Transform transform)
    {
        positionName = name;
        this.type = type;
        this.allowedUnits = allowedUnits;
        this.positionTransform = transform;
    }

    public bool CanAssignUnit(DefensiveUnit unit)
    {
        return unit.isAvailable && allowedUnits.Contains(unit.type);
    }
}

public enum PositionType
{
    Wall,
    Tower,
    Gate
}
