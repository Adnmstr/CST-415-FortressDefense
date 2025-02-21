using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This solver marks defensive positions under attack (via EnemyWave entries),
/// creates spawn assignments for each position based on its type and required spawn point names,
/// assigns available defensive units, and instantiates their prefabs at the correct spawn points.
/// </summary>
public class CSPDefenceSolver : MonoBehaviour
{
    [Header("Defensive Setup")]
    [Tooltip("Pool of available defensive units (e.g., Archers, Catapults, Knights).")]
    public List<DefensiveUnit> availableUnits;

    [Tooltip("List of fortress defensive positions (Wall, Tower, or Gate).")]
    public List<DefensivePosition> defensivePositions;

    /// <summary>
    /// Called when an enemy wave is incoming.
    /// </summary>
    /// <param name="wave">The incoming enemy wave.</param>
    public void PlanDefense(EnemyWave wave)
    {
        // Reset each defensive position and clear any previously spawned objects.
        foreach (DefensivePosition pos in defensivePositions)
        {
            pos.isUnderAttack = false;
            pos.assignedUnit = null;

            if (pos.defensiveObjects != null)
            {
                foreach (GameObject obj in pos.defensiveObjects)
                {
                    Destroy(obj);
                }
                pos.defensiveObjects.Clear();
            }
        }
        // Reset all defensive units.
        foreach (DefensiveUnit unit in availableUnits)
        {
            unit.isAvailable = true;
        }

        // Mark positions under attack based on the enemy wave entries.
        List<DefensivePosition> positionsUnderAttack = new List<DefensivePosition>();
        foreach (EnemyWaveEntry entry in wave.waveEntries)
        {
            // We assume spawnLocationIndex corresponds to an index in the defensivePositions list.
            if (entry.spawnLocationIndex >= 0 && entry.spawnLocationIndex < defensivePositions.Count)
            {
                DefensivePosition pos = defensivePositions[entry.spawnLocationIndex];
                pos.isUnderAttack = true;
                if (!positionsUnderAttack.Contains(pos))
                    positionsUnderAttack.Add(pos);
            }
        }

        // Build spawn assignments for each position under attack.
        List<SpawnAssignment> assignments = new List<SpawnAssignment>();
        foreach (DefensivePosition pos in positionsUnderAttack)
        {
            switch (pos.type)
            {
                case PositionType.Wall:
                    // Walls: one spawn point "ArcherPosition" for Archers.
                    assignments.Add(new SpawnAssignment(pos, "ArcherPosition", UnitType.Archer));
                    break;
                case PositionType.Tower:
                    // Towers: two spawn points ("ArcherPosition" for Archers and "CatapultPosition" for Catapults).
                    assignments.Add(new SpawnAssignment(pos, "ArcherPosition", UnitType.Archer));
                    assignments.Add(new SpawnAssignment(pos, "CatapultPosition", UnitType.Catapult));
                    break;
                case PositionType.Gate:
                    // Gates: two spawn points ("KnightPosition" for Knights and "CatapultPosition" for Catapults).
                    assignments.Add(new SpawnAssignment(pos, "KnightPosition", UnitType.Knight));
                    assignments.Add(new SpawnAssignment(pos, "CatapultPosition", UnitType.Catapult));
                    break;
                default:
                    Debug.LogWarning("Unknown PositionType on " + pos.positionName);
                    break;
            }
        }

        // Greedily assign available units to each spawn assignment.
        foreach (SpawnAssignment assignment in assignments)
        {
            foreach (DefensiveUnit unit in availableUnits)
            {
                if (unit.isAvailable && unit.type == assignment.allowedType)
                {
                    assignment.assignedUnit = unit;
                    unit.isAvailable = false;
                    // Optionally, record the assigned unit on the defensive position.
                    assignment.parent.assignedUnit = unit;
                    break;
                }
            }
        }

        // Instantiate the units at their respective spawn points.
        PlaceDefensiveUnits(assignments);
        Debug.Log("Defense planning completed.");
    }

    /// <summary>
    /// For each spawn assignment that received a unit, find the matching spawn point in the list and instantiate the unit prefab.
    /// </summary>
    /// <param name="assignments">List of spawn assignments.</param>
    private void PlaceDefensiveUnits(List<SpawnAssignment> assignments)
    {
        foreach (SpawnAssignment assignment in assignments)
        {
            if (assignment.assignedUnit != null)
            {
                // Search the list of spawnPoints for a Transform with the matching name.
                Transform spawnPoint = null;
                foreach (Transform sp in assignment.parent.spawnPoints)
                {
                    if (sp.name == assignment.spawnPointName)
                    {
                        spawnPoint = sp;
                        break;
                    }
                }

                if (spawnPoint != null)
                {
                    if (assignment.assignedUnit.unitPrefab != null)
                    {
                        GameObject unitInstance = Instantiate(
                            assignment.assignedUnit.unitPrefab,
                            spawnPoint.position,
                            spawnPoint.rotation
                        );
                        // Parent the spawned unit under the position's transform for organization.
                        unitInstance.transform.parent = assignment.parent.positionTransform;
                        assignment.parent.defensiveObjects.Add(unitInstance);
                    }
                    else
                    {
                        Debug.LogWarning("Unit prefab missing for " + assignment.assignedUnit.unitName);
                    }
                }
                else
                {
                    Debug.LogWarning("Spawn point '" + assignment.spawnPointName + "' not found in " + assignment.parent.positionName);
                }
            }
        }
    }

    /// <summary>
    /// Helper class representing a spawn assignment for a specific spawn point.
    /// </summary>
    private class SpawnAssignment
    {
        public DefensivePosition parent;      // The defensive position.
        public string spawnPointName;           // The exact spawn point name (e.g., "ArcherPosition").
        public UnitType allowedType;            // The type of unit allowed at this spawn point.
        public DefensiveUnit assignedUnit;      // The unit assigned (if any).

        public SpawnAssignment(DefensivePosition parent, string spawnPointName, UnitType allowedType)
        {
            this.parent = parent;
            this.spawnPointName = spawnPointName;
            this.allowedType = allowedType;
            assignedUnit = null;
        }
    }
}
