using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum PowerupType {ChangeWeapon, DestroyAllEnemies, SwitchEnemies, HealthUp, DamageUp, SlowEnemy, FastPlayer, Bomb, Drunk, Weed, Cocaine, HotPotato, StickyGun, TurretGun, fourTwenty, Disco, LSD}


/*
    Enemies explode (chain reaction?)
    - Disco mode (dancing enemies, party lights)
 */

public class Powerup : MonoBehaviour
{
    public PowerupType type;
    public float PowerupLength;
    [Space]
    public float BombRadius;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivatePowerup();
        }
    }

    public void ActivatePowerup()
    {
        StartCoroutine(DoPowerup());
    }

    IEnumerator DoPowerup()
    {
        //modify variables
        switch (type)
        {
            case PowerupType.ChangeWeapon:
                break;

            case PowerupType.DestroyAllEnemies:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(99999999999999); //ITS OVER 9000!!
                }
                break;

            case PowerupType.SwitchEnemies:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.transform.position = new Vector3(-enemy.transform.position.x, enemy.transform.position.y);
                }
                break;

            case PowerupType.HealthUp:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().Health = player.GetComponent<Player>().MaxHealth;
                }
                break;

            case PowerupType.DamageUp:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().DamageTakenModifier = 2;
                }
                break;

            case PowerupType.SlowEnemy:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().Speed /= 2;
                }
                break;

            case PowerupType.FastPlayer:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().HorizontalSpeed *= 2;
                    player.GetComponent<Player>().VerticalSpeed *= 2;
                }
                break;

            case PowerupType.Bomb:
                foreach (Collider2D enemy in Physics2D.OverlapCircleAll(transform.position, BombRadius))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(999999999999999);//ITS OVER 9000!!!
                }
                break;

            case PowerupType.Drunk:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().SetControlInversion(true);
                }
                break;
            case PowerupType.Weed:
                Time.timeScale = 0.5f;
                break;
            case PowerupType.Cocaine:
                Time.timeScale = 2;
                break;
            case PowerupType.HotPotato:
                foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 1))
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        collider.gameObject.GetComponent<Player>().HeldObject = null;
                    }
                }
                break;
            case PowerupType.StickyGun:
                GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>().Shooting = true;
                break;
            case PowerupType.TurretGun:
                foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 1))
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        collider.gameObject.GetComponent<Player>().SetCanMove(false);
                    }
                }
                break;

            case PowerupType.fourTwenty:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().SetControlInversion(true);
                }
                Time.timeScale = 0.5f;
                Camera.main.GetComponent<PostProcessVolume>().enabled = true; 
                Camera.main.GetComponent<LSD>().enabled = true;
                break;

            case PowerupType.Disco:
                Debug.Log("Partytime, Baby!!");
                break;
            case PowerupType.LSD:
                //Implement some kind of post-processing colour grading
                Camera.main.GetComponent<PostProcessVolume>().enabled = true;
                Camera.main.GetComponent<LSD>().enabled = true;
                break;

            default:
                break;
        }
        HidePowerup();

        yield return new WaitForSeconds(PowerupLength);

        //Unmodify variables
        switch (type)
        {
            case PowerupType.ChangeWeapon:
                break;

            case PowerupType.SwitchEnemies:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.transform.position = new Vector3(-enemy.transform.position.x, enemy.transform.position.y);
                }
                break;

            case PowerupType.DamageUp:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().DamageTakenModifier = 1;
                }
                break;

            case PowerupType.SlowEnemy:
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().Speed *= 2;
                }
                break;

            case PowerupType.FastPlayer:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().HorizontalSpeed /= 2;
                    player.GetComponent<Player>().VerticalSpeed /= 2;
                }
                break;



            case PowerupType.Drunk:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().SetControlInversion(false);
                }
                break;
            case PowerupType.Weed:
                Time.timeScale = 1;
                break;
            case PowerupType.Cocaine:
                Time.timeScale = 1;
                break;
            case PowerupType.TurretGun:
                foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 1))
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        collider.gameObject.GetComponent<Player>().SetCanMove(true);
                    }
                }
                break;

            case PowerupType.fourTwenty:
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().SetControlInversion(false);
                }
                Time.timeScale = 1;
                Camera.main.GetComponent<PostProcessVolume>().enabled = false;
                Camera.main.GetComponent<LSD>().enabled = false;
                break;

            case PowerupType.Disco:
                Debug.Log("Un-Partytime, Baby!!");
                break;
            case PowerupType.LSD:
                Camera.main.GetComponent<PostProcessVolume>().enabled = false;
                Camera.main.GetComponent<LSD>().enabled = false;
                break;

            default:
                break;
        }
        Destroy(gameObject);
        
    }
    public void HidePowerup()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
