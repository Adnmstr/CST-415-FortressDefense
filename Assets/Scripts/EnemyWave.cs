using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [Tooltip("Wave number for reference.")]
    public int waveNumber;

    [Tooltip("Arrival time (unused when rounds are externally coordinated).")]
    public float arrivalTime;

    [Tooltip("List of enemy wave entries that define the wave.")]
    public List<EnemyWaveEntry> waveEntries;
}

[System.Serializable]
public class EnemyWaveEntry
{
    [Tooltip("Index into WaveManager.enemyPrefabs.")]
    public int enemyTypeIndex;

    [Tooltip("Number of enemies of this type to spawn.")]
    public int enemyCount;

    [Tooltip("Index into WaveManager.spawnLocations (the lane for these enemies).")]
    public int spawnLocationIndex;
}
