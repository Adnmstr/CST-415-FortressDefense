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

    [Tooltip("List of spawn points available for this position. Each Transform should be named exactly as required (e.g., 'ArcherPosition').")]
    public List<Transform> spawnPoints;

    [Tooltip("The defensive unit assigned to this position (if any).")]
    public DefensiveUnit assignedUnit;

    /// <summary>
    /// Indicates if this position is currently under attack.
    /// </summary>
    [HideInInspector]
    public bool isUnderAttack = false;

    /// <summary>
    /// Once defensive units are assigned, their visual representations (if instantiated) are stored here.
    /// </summary>
    [HideInInspector]
    public List<GameObject> defensiveObjects = new List<GameObject>();

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
