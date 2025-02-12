using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Attacker Setup")]
    [Tooltip("List of enemy prefabs available to the attacker.")]
    public List<GameObject> enemyPrefabs;

    [Tooltip("List of spawn locations (lanes) for attackers.")]
    public List<Transform> spawnLocations;

    [Tooltip("Base enemy count used in wave generation.")]
    public float baseEnemyCount = 3f;

    [Tooltip("Current wave number (increases with reinforcements).")]
    public int currentWaveNumber = 1;

    /// <summary>
    /// Generates a new enemy wave based on the current wave number.
    /// For each spawn location, a random decision is made as to whether that lane is attacked.
    /// </summary>
    public EnemyWave GenerateWave()
    {
        EnemyWave wave = new EnemyWave();
        wave.waveNumber = currentWaveNumber;
        // Note: arrivalTime is not used when rounds are externally coordinated.
        wave.arrivalTime = 0f;
        wave.waveEntries = new List<EnemyWaveEntry>();

        // For each lane, decide whether to send an attack.
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (Random.value > 0.5f)  // 50% chance of an attack in this lane.
            {
                EnemyWaveEntry entry = new EnemyWaveEntry();
                entry.spawnLocationIndex = i;
                entry.enemyTypeIndex = Random.Range(0, enemyPrefabs.Count);
                // The enemy count scales with the current wave plus a bit of randomness.
                entry.enemyCount = Mathf.RoundToInt(baseEnemyCount * currentWaveNumber * (0.8f + Random.value * 0.4f));
                wave.waveEntries.Add(entry);
            }
        }
        return wave;
    }

    /// <summary>
    /// Spawns all enemy units described in the given wave.
    /// </summary>
    public void SpawnWave(EnemyWave wave)
    {
        foreach (EnemyWaveEntry entry in wave.waveEntries)
        {
            // Validate indices.
            if (entry.enemyTypeIndex < 0 || entry.enemyTypeIndex >= enemyPrefabs.Count ||
                entry.spawnLocationIndex < 0 || entry.spawnLocationIndex >= spawnLocations.Count)
            {
                Debug.LogWarning("Wave entry contains invalid indices.");
                continue;
            }

            for (int i = 0; i < entry.enemyCount; i++)
            {
                Vector3 spawnPos = spawnLocations[entry.spawnLocationIndex].position;
                Quaternion spawnRot = spawnLocations[entry.spawnLocationIndex].rotation;
                GameObject enemy = Instantiate(enemyPrefabs[entry.enemyTypeIndex], spawnPos, spawnRot);

                // Optionally configure the enemy (e.g., assign target transforms).
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    // enemyAI.targetPosition can be set later by other logic.
                }
            }
        }
    }

    /// <summary>
    /// Reinforces the attacker between rounds by increasing the wave number.
    /// </summary>
    public void ReinforceAttacker()
    {
        currentWaveNumber++;
        Debug.Log("Attacker reinforced. Next wave number: " + currentWaveNumber);
    }
}
