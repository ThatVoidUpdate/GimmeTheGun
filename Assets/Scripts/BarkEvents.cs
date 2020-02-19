using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public enum BarkEventTypes {PowerupPickup, KillEnemy, NewRound, HalfHealth, NearDeath, Death, WaveCleared, NewWeapon, ThrowGun, NewSkin, NewPowerup, Achievement, Boss}

public class BarkEvents : MonoBehaviour
{
    public string BarkLinesFile;
    private Dictionary<BarkEventTypes, string[]> AllLines = new Dictionary<BarkEventTypes, string[]>();

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = File.ReadAllLines(BarkLinesFile);
        lines = (from line in lines where !line.StartsWith("#") select line).ToArray();
        lines = (from line in lines where line!="" select line).ToArray();
        foreach (string line in lines)
        {
            print(line);
        }
    }

    public static void TriggerBarkLine(BarkEventTypes type)
    {

    }
}
