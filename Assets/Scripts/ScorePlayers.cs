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
        if (enemy.transform.position.x > 0)
        {//enemy is on the right
            RightScore += enemy.ScoreValue;
        }
        else if (enemy.transform.position.x < 0)
        {//enemy is on the right
            LeftScore += enemy.ScoreValue;
        }
        else
        {
            //wat
        }

        LeftScoreText.text = "Left score" + LeftScore.ToString();
        RightScoreText.text = "Right score" + RightScore.ToString();
    }
    public void PlayerKills(Player player)
    {
        if (player.transform.position.x > 0)
        {//enemy is on the right
            RightScore += player.DeathScore;
        }
        else if (player.transform.position.x < 0)
        {//enemy is on the right
            LeftScore += player.DeathScore;
        }
        else
        {
            //wat
        }

        LeftScoreText.text = "Left score" + LeftScore.ToString();
        RightScoreText.text = "Right score" + RightScore.ToString();
    }

}