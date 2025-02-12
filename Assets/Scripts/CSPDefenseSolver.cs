// Implements the CSP solver using Backtracking Search.

/*
using System.Collections.Generic;
using UnityEngine;

public class CSPDefenseSolver : MonoBehaviour
{
    public List<DefensiveUnit> availableUnits;
    public List<DefensivePosition> defensivePositions;
    public List<EnemyWave> enemyWaves;
    public GameObject positionObject; // added this so as to fix an error

    private Dictionary<DefensivePosition, DefensiveUnit> assignedUnits = new Dictionary<DefensivePosition, DefensiveUnit>();

   void Start()
{
        UIManager uiManager = FindObjectOfType<UIManager>();

    if (SolveCSP(0))
    {
        Debug.Log("Defense successfully assigned!");
        uiManager.UpdateAssignmentUI();
    }
    else
    {
        Debug.Log("No valid defense assignments found.");
    }

    StartCoroutine(HandleEnemyWaves()); // Start real-time updates
}

    // Coroutine to handle real-time enemy waves
    public GameObject enemyPrefab; // Assign in Unity Inspector

    
    System.Collections.IEnumerator HandleEnemyWaves()
    {
        foreach (var wave in enemyWaves)
        {

            yield return new WaitForSeconds(wave.arrivalTime); // Wait for wave arrival
            Debug.Log($"Wave {wave.waveNumber} has arrived!");

            foreach (var target in wave.attackTargets)
            {
                // Spawn enemy and set target
                GameObject enemy = Instantiate(enemyPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
                EnemyAI ai = enemy.GetComponent<EnemyAI>();
                DefensivePosition targetPosition = defensivePositions.Find(p => p.positionName == target.Key);
                ai.targetPosition = targetPosition.positionTransform;
            }

            bool reallocationNeeded = CheckBreaches(wave);
            if (reallocationNeeded)
            {
                Debug.Log("Reallocating defenses due to enemy breach...");
                SolveCSP(0);
                FindObjectOfType<UIManager>().UpdateAssignmentUI();
            }
        }
    }


    // Simulates checking for breaches
    bool CheckBreaches(EnemyWave wave)
{
    // Example: If a gate is attacked and has no defense, trigger reallocation
    foreach (var target in wave.attackTargets)
    {
        DefensivePosition position = defensivePositions.Find(p => p.positionName == target.Key);
        if (position.assignedUnit == null)
        {
            Debug.Log($"Breach detected at {target.Key}!");
            return true; // Trigger reallocation
        }
    }
    return false;
}


    bool SolveCSP(int index)
    {
        if (index >= defensivePositions.Count)
            return true; // All positions assigned

        DefensivePosition position = defensivePositions[index];

        foreach (var unit in availableUnits)
        {
            if (position.CanAssignUnit(unit))
            {
                // Assign unit and mark it unavailable
                position.assignedUnit = unit;
                unit.isAvailable = false;
                assignedUnits[position] = unit;

                if (SolveCSP(index + 1))
                    return true; // Continue solving

                // Backtrack
                position.assignedUnit = null;
                unit.isAvailable = true;
                assignedUnits.Remove(position);
            }
        }
        return false;
    }

    void DisplayAssignments()
    {
        foreach (var entry in assignedUnits)
        {
            Debug.Log($"{entry.Value.unitName} assigned to {entry.Key.positionName}");
        }
    }

}
*/