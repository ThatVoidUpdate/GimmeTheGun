using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public Controller controller;
    public Control NextPlayer;
    public Control PrevPlayer;
    public Player[] AllPlayers;
    public int CurrentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetControlDown(NextPlayer))
        {
            AllPlayers[CurrentPlayer].SetController(null);
            CurrentPlayer++;
            CurrentPlayer = CurrentPlayer % AllPlayers.Length;
            AllPlayers[CurrentPlayer].SetController(controller);
        }
        else if (controller.GetControlDown(PrevPlayer))
        {
            AllPlayers[CurrentPlayer].SetController(null);
            CurrentPlayer--;
            CurrentPlayer = CurrentPlayer == -1 ? AllPlayers.Length - 1 : CurrentPlayer;
            AllPlayers[CurrentPlayer].SetController(controller);
        }
    }
}
