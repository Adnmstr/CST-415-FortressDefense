using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefensivePosition : MonoBehaviour
{
    [Tooltip("Unique name (e.g., 'Gate 1').")]
    public string positionName;

    [Tooltip("The type of fortress element (Wall, Tower, or Gate).")]
    public PositionType type;

    [Tooltip("List of allowed unit types for this position.")]
    public List<UnitType> allowedUnits;

    [Tooltip("Transform representing the physical location of this position (an empty GameObject).")]
    public Transform positionTransform;

    [Tooltip("The defensive unit assigned to this position (if any).")]
    public DefensiveUnit assignedUnit;

    /// <summary>
    /// Indicates if this position is currently under attack.
    /// </summary>
    [HideInInspector]
    public bool isUnderAttack = false;

    /// <summary>
    /// Once a defensive unit is assigned, its visual representation (if instantiated) is stored here.
    /// </summary>
    [HideInInspector]
    public GameObject defensiveObject;

    /// <summary>
    /// Checks if the given unit can be assigned to this position.
    /// </summary>
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
