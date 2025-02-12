using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Round Coordination")]
    public WaveManager waveManager;
    public CSPDefenceSolver defenceManager;
    public float roundDuration = 30f;

    private int roundNumber = 1;
    public int RoundNumber { get { return roundNumber; } }  // Public accessor for UI

    void Start()
    {
        StartCoroutine(RoundLoop());
    }

    IEnumerator RoundLoop()
    {
        while (true)
        {
            Debug.Log("=== Starting Round " + roundNumber + " ===");

            // Phase 1: Generate the enemy wave (attacker's plan).
            EnemyWave currentWave = waveManager.GenerateWave();
            Debug.Log("Attacker generated wave " + currentWave.waveNumber +
                      " with " + currentWave.waveEntries.Count + " entry(ies).");

            // Phase 2: Defender plans defense based on the incoming wave.
            defenceManager.PlanDefense(currentWave);

            // (Optional) Update the UI now that planning is complete.
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
            {
                ui.RefreshUI(currentWave);
            }

            // Phase 3: Run the round by spawning the enemies.
            waveManager.SpawnWave(currentWave);
            Debug.Log("Enemies spawned. Battle round in progress...");

            // Wait for the round to run.
            yield return new WaitForSeconds(roundDuration);

            Debug.Log("=== Round " + roundNumber + " ended. ===");

            // Post-round: reinforce the attacker.
            waveManager.ReinforceAttacker();

            roundNumber++;
            yield return new WaitForSeconds(5f);
        }
    }
}
