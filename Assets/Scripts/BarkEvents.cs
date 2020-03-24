using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public enum BarkEventTypes {Error, PowerupPickup, KillEnemy, NewRound, HalfHealth, NearDeath, Death, WaveCleared, NewWeapon, ThrowGun, NewSkin, NewPowerup, Achievement, Boss, GunPickup, Respawn}

[System.Serializable]
public class SendBarkEvent : UnityEvent<BarkEventTypes, GameObject> { } // Can pass a GameObject as an argument in the event

public class BarkEvents : MonoBehaviour
{
    public string BarkLinesFile;
    public string FallbackBarkLineFile;

    private Dictionary<BarkEventTypes, (float, List<string>)> AllLines = new Dictionary<BarkEventTypes, (float, List<string>)>();
    //type of event, chance that a line will show up, list of all the possible lines

    public BarkBubble bubble;

    void Start()
    {
        string FilePath;
        if (File.Exists(BarkLinesFile))
        {
            FilePath = BarkLinesFile;
        }
        else
        {
            FilePath = FallbackBarkLineFile;
        }

        string[] lines = File.ReadAllLines(FilePath);
        lines = (from line in lines where !line.StartsWith("#") select line).ToArray();
        lines = (from line in lines where line != "" select line).ToArray();

        BarkEventTypes currentType = BarkEventTypes.Error;

        foreach (BarkEventTypes type in Enum.GetValues(typeof(BarkEventTypes)))
        {
            AllLines.Add(type, (0, new List<string>()));
        }

        foreach (string line in lines)
        {
            if (line.StartsWith("[") && line.Contains("]"))
            {
                if (!Enum.TryParse(line.Split(']')[0].Substring(1, line.Split(']')[0].Length - 1), out currentType))
                {
                    Debug.LogWarning("Event " + line.Split(']')[0].Substring(1, line.Split(']')[0].Length - 1) + " does not exist (" + FilePath + ")");
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
