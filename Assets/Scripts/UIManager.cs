using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro UI

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI assignmentText; // Assign in Unity Editor
    public TextMeshProUGUI enemyWaveText; // Assign in Unity Editor
    private CSPDefenseSolver cspSolver;

    void Start()
    {
        cspSolver = FindObjectOfType<CSPDefenseSolver>();
        UpdateAssignmentUI();
        UpdateEnemyWaveUI();
    }

    public void UpdateAssignmentUI()
    {
        string displayText = "<b>Defense Assignments:</b>\n";
        foreach (var position in cspSolver.defensivePositions)
        {
            string unitName = position.assignedUnit != null ? position.assignedUnit.unitName : "None";
            displayText += $"{position.positionName}: {unitName}\n";
        }
        assignmentText.text = displayText;
    }

    public void UpdateEnemyWaveUI()
    {
        string displayText = "<b>Enemy Attacks:</b>\n";
        foreach (var wave in cspSolver.enemyWaves)
        {
            displayText += $"Wave {wave.waveNumber} at {wave.arrivalTime} sec:\n";
            foreach (var target in wave.attackTargets)
            {
                displayText += $"{target.Key} ({target.Value})\n";
            }
            displayText += "\n";
        }
        enemyWaveText.text = displayText;
    }

    public void RefreshUI()
    {
        UpdateAssignmentUI();
        UpdateEnemyWaveUI();
    }
}
