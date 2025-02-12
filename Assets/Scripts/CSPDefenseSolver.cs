using System.Collections.Generic;
using UnityEngine;

public class CSPDefenceSolver : MonoBehaviour
{
    [Header("Defensive Setup")]
    [Tooltip("Pool of available defensive units (e.g., archers, knights, catapults).")]
    public List<DefensiveUnit> availableUnits;

    [Tooltip("List of fortress defensive positions (walls, towers, gates).")]
    public List<DefensivePosition> defensivePositions;

    /// <summary>
    /// Plans the defense for the incoming enemy wave.
    /// Marks which fortress positions are under attack and then uses a CSP assignment
    /// to allocate available defensive units to those positions.
    /// </summary>
    public void PlanDefense(EnemyWave wave)
    {
        // Reset all positions.
        foreach (DefensivePosition pos in defensivePositions)
        {
            pos.assignedUnit = null;
            pos.isUnderAttack = false;
        }

        // For simplicity, assume the enemy wave’s spawn location index corresponds to a defensive position.
        List<DefensivePosition> positionsUnderAttack = new List<DefensivePosition>();
        foreach (EnemyWaveEntry entry in wave.waveEntries)
        {
            if (entry.spawnLocationIndex >= 0 && entry.spawnLocationIndex < defensivePositions.Count)
            {
                DefensivePosition pos = defensivePositions[entry.spawnLocationIndex];
                pos.isUnderAttack = true;
                if (!positionsUnderAttack.Contains(pos))
                    positionsUnderAttack.Add(pos);
            }
        }

        // Use backtracking to assign available units to the positions under attack.
        if (SolveDefenseCSP(positionsUnderAttack, 0))
        {
            Debug.Log("Defense planning succeeded.");
            // Now instantiate the visual representations.
            PlaceDefensiveUnits();
        }
        else
            Debug.LogWarning("Defense planning failed. Some positions remain undefended.");
    }

    /// <summary>
    /// Recursively assigns available defensive units to the given positions.
    /// </summary>
    private bool SolveDefenseCSP(List<DefensivePosition> positions, int index)
    {
        if (index >= positions.Count)
            return true; // All positions assigned.

        DefensivePosition pos = positions[index];
        foreach (DefensiveUnit unit in availableUnits)
        {
            if (unit.isAvailable && pos.allowedUnits.Contains(unit.type))
            {
                // Tentatively assign the unit.
                pos.assignedUnit = unit;
                unit.isAvailable = false;

                if (SolveDefenseCSP(positions, index + 1))
                    return true;

                // Backtrack.
                pos.assignedUnit = null;
                unit.isAvailable = true;
            }
        }
        return false;
    }

    /// <summary>
    /// Instantiates (or moves) the defensive unit prefab to the corresponding defensive position.
    /// </summary>
    private void PlaceDefensiveUnits()
    {
        foreach (DefensivePosition pos in defensivePositions)
        {
            if (pos.assignedUnit != null)
            {
                // If we haven't placed a unit yet, or we want to update its position:
                if (pos.defensiveObject == null && pos.assignedUnit.unitPrefab != null)
                {
                    pos.defensiveObject = Instantiate(
                        pos.assignedUnit.unitPrefab,
                        pos.positionTransform.position,
                        pos.positionTransform.rotation
                    );
                    // Optionally parent the object so the hierarchy remains organized.
                    pos.defensiveObject.transform.parent = pos.positionTransform;
                }
                else if (pos.defensiveObject != null)
                {
                    // Update position/rotation if needed.
                    pos.defensiveObject.transform.position = pos.positionTransform.position;
                    pos.defensiveObject.transform.rotation = pos.positionTransform.rotation;
                }
            }
        }
    }
}
