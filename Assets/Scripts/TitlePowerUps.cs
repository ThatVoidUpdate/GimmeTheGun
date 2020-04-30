using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitlePowerUps : MonoBehaviour
{
    public Animator animator;
    private TextMeshProUGUI text;

    public IEnumerator TitlePowerUp (PowerupType type)
    {
        switch (type)
        {
            case PowerupType.ChangeWeapon:
                text.text = "change weapon";
                break;
            case PowerupType.DestroyAllEnemies:
                text.text = "Destroy all enemies";
                break;
            case PowerupType.SwitchEnemies:
                text.text = "Switch enemies";
                break;
            case PowerupType.HealthUp:
                text.text = "Health up";
                break;
            case PowerupType.DamageUp:
                text.text = "Damage up";
                break;
            case PowerupType.SlowEnemy:
                text.text = "Slow enemy";
                break;
            case PowerupType.FastPlayer:
                text.text = "Fast player";
                break;
            case PowerupType.Bomb:
                text.text = "Bomb";
                break;
            case PowerupType.Drunk:
                text.text = "Drunk";
                break;
            case PowerupType.Weed:
                text.text = "Weed";
                break;
            case PowerupType.Cocaine:
                text.text = "Cocaine";
                break;
            case PowerupType.HotPotato:
                text.text = "Hot potato";
                break;
            case PowerupType.StickyGun:
                text.text = "Sticky gun";
                break;
            case PowerupType.fourTwenty:
                text.text = "four twenty";
                break;
            case PowerupType.Disco:
                text.text = "Disco";
                break;
            case PowerupType.LSD:
                text.text = "LSD";
                break;
            default:
                break;
        }
        animator.SetTrigger("Entry");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Exit");
    }

    public void Start()
    {

        text = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
       
    }
}
