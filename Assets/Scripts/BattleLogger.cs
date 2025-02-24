using UnityEngine;
using System.IO;

public class BattleLogger : MonoBehaviour
{
    public static BattleLogger Instance;

    private string filePath;

    private void Awake()
    {
        // Ensure only one instance of BattleLogger exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep BattleLogger between scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }

        // Define the path to save the CSV file
        filePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "BattleLog.csv");
        Debug.Log("Battle log saved at: " + filePath);


        // If the file doesn't exist, create it and add the headers
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))  // 'false' means don't append
            {
                writer.WriteLine("Attacker, Victim, Time");
            }
        }
    }

    // Method to log kill events to the CSV file
    public void LogKill(string attacker, string victim)
    {
        // Log information into the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))  // 'true' means append
        {
            writer.WriteLine($"{attacker}, {victim}, {Time.time}");
        }

        // Optionally, print to console for debugging
        Debug.Log($"Logged Kill: {attacker} killed {victim} at {Time.time}");
    }
}
