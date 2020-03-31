using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScorePlayers : MonoBehaviour
{
    private int RightScore;
    private int LeftScore;
    public Text LeftScoreText;
    public Text RightScoreText;


    public void EnemyKill(Enemy enemy)
    {
        if (enemy.LastDamagedBy != null)
        {
            if (enemy.LastDamagedBy.ID == PlayerID.Left)
            {
                LeftScore += enemy.ScoreValue;
            }
            else
            {
                RightScore += enemy.ScoreValue;
            }
        }

        LeftScoreText.text = "Score: " + LeftScore.ToString();
        RightScoreText.text = "Score: " + RightScore.ToString();
    }
    public void PlayerDies(Player player)
    {
        if (player.ID == PlayerID.Left)
        {
            LeftScore += player.DeathScorePenalty;
        }
        else
        {
            RightScore += player.DeathScorePenalty;
        }

        LeftScoreText.text = "Score: " + LeftScore.ToString();
        RightScoreText.text = "Score: " + RightScore.ToString();
    }

}