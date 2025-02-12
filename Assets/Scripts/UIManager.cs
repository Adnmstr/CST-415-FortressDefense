using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Text element to display defense assignments.")]
    public TextMeshProUGUI assignmentText;

    [Tooltip("Text element to display enemy wave information.")]
    public TextMeshProUGUI enemyWaveText;

    [Tooltip("Text element to display round info (e.g., round number).")]
    public TextMeshProUGUI roundInfoText;

    [Header("References")]
    [Tooltip("Reference to the CSPDefenceSolver (defense manager).")]
    public CSPDefenceSolver defenceManager;

    [Tooltip("Reference to the WaveManager (attacker manager).")]
    public WaveManager waveManager;

    [Tooltip("Reference to the RoundManager.")]
    public RoundManager roundManager;

    /// <summary>
    /// Call this method to refresh all UI elements. Typically, this is called
    /// at the end of the planning phase or when a new round begins.
    /// Pass in the current enemy wave so that its details can be displayed.
    /// </summary>
    public void RefreshUI(EnemyWave currentWave)
    {
        UpdateAssignmentUI();
        UpdateEnemyWaveUI(currentWave);
        UpdateRoundInfoUI();
    }

    /// <summary>
    /// Updates the UI text that displays the defensive assignments.
    /// </summary>
    void UpdateAssignmentUI()
    {
        string displayText = "<b>Defense Assignments:</b>\n";
        if (defenceManager != null && defenceManager.defensivePositions != null)
        {
            foreach (var pos in defenceManager.defensivePositions)
            {
                string unitName = pos.assignedUnit != null ? pos.assignedUnit.unitName : "None";
                displayText += $"{pos.positionName}: {unitName}\n";
            }
        }
        assignmentText.text = displayText;
    }

    /// <summary>
    /// Updates the UI text that displays enemy wave details.
    /// </summary>
    /// <param name="wave">The enemy wave that is about to run.</param>
    void UpdateEnemyWaveUI(EnemyWave wave)
    {
        string displayText = "<b>Enemy Wave Info:</b>\n";
        if (wave != null && wave.waveEntries != null)
        {
            displayText += $"Wave {wave.waveNumber}:\n";
            foreach (var entry in wave.waveEntries)
            {
                // Try to get a friendly name for the enemy type.
                string enemyName = "Unknown";
                if (waveManager != null &&
                    waveManager.enemyPrefabs != null &&
                    entry.enemyTypeIndex >= 0 &&
                    entry.enemyTypeIndex < waveManager.enemyPrefabs.Count)
                {
                    enemyName = waveManager.enemyPrefabs[entry.enemyTypeIndex].name;
                }
                displayText += $"Lane {entry.spawnLocationIndex}: {entry.enemyCount} x {enemyName}\n";
            }
        }
        enemyWaveText.text = displayText;
    }

    /// <summary>
    /// Updates the UI text that displays the current round number.
    /// Assumes the RoundManager provides a public property (RoundNumber) to access the current round.
    /// </summary>
    void UpdateRoundInfoUI()
    {
        string displayText = "<b>Round:</b> ";
        if (roundManager != null)
        {
            displayText += roundManager.RoundNumber.ToString();
        }
        else
        {
            displayText += "N/A";
        }
        roundInfoText.text = displayText;
    }
}
