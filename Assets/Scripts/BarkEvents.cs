using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;
using System.Collections;

public enum BarkEventTypes {Error, PowerupPickup, KillEnemy, NewRound, HalfHealth, NearDeath, Death, WaveCleared, NewWeapon, ThrowGun, NewSkin, NewPowerup, Achievement, Boss}

public class BarkEvents : MonoBehaviour
{
    public string BarkLinesFile;
    private Dictionary<BarkEventTypes, (float, List<string>)> AllLines = new Dictionary<BarkEventTypes, (float, List<string>)>();

    public BarkBubble bubble;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = File.ReadAllLines(BarkLinesFile);
        lines = (from line in lines where !line.StartsWith("#") select line).ToArray();
        lines = (from line in lines where line!="" select line).ToArray();

        BarkEventTypes currentType = BarkEventTypes.Error;

        foreach (BarkEventTypes type in Enum.GetValues(typeof(BarkEventTypes)))
        {
            AllLines.Add(type, (0, new List<string>()));
        }

        foreach (string line in lines)
        {
            if (line.StartsWith("[") && line.Contains("]"))
            {               
                switch (line.Split(']')[0].Substring(1, line.Split(']')[0].Length - 1))
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
                        Debug.LogError("Invalid event in Bark Event Parser file: " + BarkLinesFile + ", \"" + line + "\"");
                        currentType = BarkEventTypes.Error;
                        break;
                }
                AllLines[currentType] = ((float)Convert.ToDouble(line.Split(']')[1]), AllLines[currentType].Item2);
            }
            else
            {
                AllLines[currentType].Item2.Add(line);
            }
        }
    }

    public void TriggerBarkLine(BarkEventTypes type, GameObject Player)
    {
        if (AllLines[type].Item2.Count > 0)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) < AllLines[type].Item1)
            {
                string DisplayLine = AllLines[type].Item2[UnityEngine.Random.Range(0, AllLines[type].Item2.Count)];
                StartCoroutine(ShowBarkLine(DisplayLine, 2, Player));
            }
            
        }
    }

    IEnumerator ShowBarkLine(string Line, float WaitTime, GameObject Player)
    {
        bubble.gameObject.SetActive(true);
        bubble.GetComponentInChildren<TextMeshProUGUI>().text = Line;
        bubble.TrackingObject = Player;
        yield return new WaitForSeconds(WaitTime);
        bubble.gameObject.SetActive(false);
    }
}
