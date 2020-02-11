using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject PowerupPrefab;
    public float SecondsBetweenPowerups;
    private BoxCollider2D SpawnArea;
    private float CurrentTime = 0;
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
            PowerupType type = (PowerupType)Random.Range(0, System.Enum.GetNames(typeof(PowerupType)).Length); //Choose a random type of powerup
            //PowerupType type = PowerupType.Weed;
            Vector3 Position = new Vector3(Random.Range(SpawnArea.offset.x - SpawnArea.size.x / 2, SpawnArea.offset.x + SpawnArea.size.x / 2), Random.Range(SpawnArea.offset.y - SpawnArea.size.y / 2, SpawnArea.offset.y + SpawnArea.size.y / 2)); //Choose a random position
            GameObject CurrentPowerup = Instantiate(PowerupPrefab, Position, Quaternion.identity); //Spawn the powerup
            CurrentPowerup.GetComponent<Powerup>().type = type;//Set the type of the powerup

            CurrentTime -= SecondsBetweenPowerups;
        }
    }
}
