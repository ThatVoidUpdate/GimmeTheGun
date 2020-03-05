using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum PowerupType {ChangeWeapon, DestroyAllEnemies, SwitchEnemies, HealthUp, DamageUp, SlowEnemy, FastPlayer, Bomb, Drunk, Weed, Cocaine, HotPotato, StickyGun, fourTwenty, Disco, LSD}


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
    [Space]
    [Tooltip("All the guns in the game, to be used for the ChangeWeapons powerup")]
    public GameObject[] Guns;

    void Start()
    {
        switch (type)
        {
            case PowerupType.ChangeWeapon:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/WeaponSwitch");
                break;
            case PowerupType.DestroyAllEnemies:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Nuke");
                break;
            case PowerupType.SwitchEnemies:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bottle");
                break;
            case PowerupType.HealthUp:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/HealthUp");
                break;
            case PowerupType.DamageUp:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/DamageUp");
                break;
            case PowerupType.SlowEnemy:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bottle");
                break;
            case PowerupType.FastPlayer:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Player_clock_1");
                break;
            case PowerupType.Bomb:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bomb");
                break;
            case PowerupType.Drunk:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/DrunkMode");
                break;
            case PowerupType.Weed:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Weed");
                break;
            case PowerupType.Cocaine:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Cocaine");
                break;
            case PowerupType.HotPotato:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/HotPotato");
                break;
            case PowerupType.StickyGun:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bottle");
                break;
            case PowerupType.fourTwenty:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bottle");
                break;
            case PowerupType.Disco:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/NewPartyHat");
                break;
            case PowerupType.LSD:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/PowerUps/Bottle");
                break;
            default:
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.PowerupPickup, collision.gameObject);
            StartCoroutine(DoPowerup());
        }
    }

    IEnumerator DoPowerup()
    {
        print("Collected powerup: " + type);

        StartCoroutine(FindObjectOfType<TitlePowerUps>().TitlePowerUp(type)); 

        //modify variables
        switch (type)
        {
            case PowerupType.ChangeWeapon:
                GameObject oldGun = GameObject.FindObjectOfType<Gun>().gameObject;
                GameObject newGunPrefab = Guns[Random.Range(0, Guns.Length)];
                GameObject newGun = Instantiate(newGunPrefab, oldGun.transform.position, oldGun.transform.rotation);
                foreach (Player player in GameObject.FindObjectsOfType<Player>())
                {
                    if (player.HeldObject != null)
                    {
                        player.HeldObject = newGun.GetComponent<Gun>();
                        player.closeGuns.Clear();
                        player.closeGuns.Add(newGun.GetComponent<Collider2D>());

                        break;
                    }
                }
                Destroy(oldGun);

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
                    player.GetComponent<Player>().Respawn();
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
                try
                {
                    foreach (Collider2D enemy in Physics2D.OverlapCircleAll(transform.position, BombRadius))
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(999999999999999);//ITS OVER 9000!!!
                    }
                }
                catch { }
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
                foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
                {
                    enemy.Spin(true);
                }
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
                foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
                {
                    enemy.Spin(false);
                }
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
        print("Hiding powerup");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
