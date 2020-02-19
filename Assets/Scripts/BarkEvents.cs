using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public enum BarkEventTypes {PowerupPickup, KillEnemy, NewRound, HalfHealth, NearDeath, Death, WaveCleared, NewWeapon, ThrowGun, NewSkin, NewPowerup, Achievement, Boss}

public class BarkEvents : MonoBehaviour
{
    public string BarkLinesFile;
    private Dictionary<BarkEventTypes, List<string>> AllLines = new Dictionary<BarkEventTypes, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = File.ReadAllLines(BarkLinesFile);
        lines = (from line in lines where !line.StartsWith("#") select line).ToArray();
        lines = (from line in lines where line!="" select line).ToArray();

        BarkEventTypes currentType = BarkEventTypes.PowerupPickup;

        foreach (BarkEventTypes type in Enum.GetValues(typeof(BarkEventTypes)))
        {
            AllLines.Add(type, new List<string>());
        }

        foreach (string line in lines)
        {
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                switch (line.Substring(1, line.Length - 2))
                {
                    case "PowerupPickup":
                        currentType = BarkEventTypes.PowerupPickup;
                        break;
                    case "KillEnemy":
                        currentType = BarkEventTypes.KillEnemy;
                        break;
                    case "NewRound":
                        currentType = BarkEventTypes.NewRound;
                        break;
                    case "HalfHealth":
                        currentType = BarkEventTypes.HalfHealth;
                        break;
                    case "NearDeath":
                        currentType = BarkEventTypes.NearDeath;
                        break;
                    case "Death":
                        currentType = BarkEventTypes.Death;
                        break;
                    case "WaveCleared":
                        currentType = BarkEventTypes.WaveCleared;
                        break;
                    case "NewWeapon":
                        currentType = BarkEventTypes.NewWeapon;
                        break;
                    case "ThrowGun":
                        currentType = BarkEventTypes.ThrowGun;
                        break;
                    case "NewSkin":
                        currentType = BarkEventTypes.NewSkin;
                        break;
                    case "NewPowerup":
                        currentType = BarkEventTypes.NewPowerup;
                        break;
                    case "Achievement":
                        currentType = BarkEventTypes.Achievement;
                        break;
                    case "Boss":
                        currentType = BarkEventTypes.Boss;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                AllLines[currentType].Add(line);
            }
        }

        foreach (string line in AllLines[BarkEventTypes.NewWeapon])
        {
            print(line);
        }
    }

    public static void TriggerBarkLine(BarkEventTypes type)
    {

    }
}
