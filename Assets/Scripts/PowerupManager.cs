using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject PowerupPrefab;
    public float SecondsBetweenPowerups;
    private BoxCollider2D SpawnArea;
    private float CurrentTime = 0;

    private PowerupType[] chances = new PowerupType[] { PowerupType.ChangeWeapon, PowerupType.ChangeWeapon, PowerupType.DestroyAllEnemies, PowerupType.SwitchEnemies, PowerupType.HealthUp, PowerupType.DamageUp, PowerupType.SlowEnemy, PowerupType.FastPlayer, PowerupType.Bomb, PowerupType.Drunk, PowerupType.Weed, PowerupType.Cocaine, PowerupType.HotPotato, PowerupType.StickyGun, PowerupType.fourTwenty, PowerupType.Disco, PowerupType.LSD };
    // Start is called before the first frame update
    void Start()
    {
        SpawnArea = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > SecondsBetweenPowerups)
        {//Spawn a new powerup
            PowerupType type = chances[Random.Range(0, chances.Length)]; //Choose a random type of powerup
            //PowerupType type = PowerupType.Weed;
            Vector3 Position = new Vector3(Random.Range(SpawnArea.offset.x - SpawnArea.size.x / 2, SpawnArea.offset.x + SpawnArea.size.x / 2), Random.Range(SpawnArea.offset.y - SpawnArea.size.y / 2, SpawnArea.offset.y + SpawnArea.size.y / 2)); //Choose a random position
            GameObject CurrentPowerup = Instantiate(PowerupPrefab, Position, Quaternion.identity); //Spawn the powerup
            CurrentPowerup.GetComponent<Powerup>().type = type;//Set the type of the powerup

            CurrentTime -= SecondsBetweenPowerups;
        }
    }
}
