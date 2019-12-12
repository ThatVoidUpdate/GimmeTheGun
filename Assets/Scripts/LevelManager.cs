using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Inner Walls")]
    public float innerWallWidth = 0.1f;
    public GameObject innerWall;
    

    [Header("Players")]
    public GameObject playerPrefab;
    public GameObject controllerObject;
    [Space]
    public List<GameObject> players = new List<GameObject>();

    [Space]
    public int playerCount = 2;
    public Vector3 gunOffset;

    [Space]
    public GameObject gunPrefab;
    public GameObject enemySpawnerPrefab;

    private float gameWidthLessWalls;
    private float gameWidth;
    private float gameHeight;

    // Start is called before the first frame update
    void Start()
    {
        gameHeight = Camera.main.orthographicSize * 2;
        gameWidth = gameHeight * Camera.main.aspect;

        if (playerCount < 2)
        {
            Debug.LogError("Player count less than 2, which is not allowed. Resetting to 2");
            playerCount = 2;
        }
        else if (playerCount > 4)
        {
            Debug.LogError("Player count greater than 4, which is not allowed. Resetting to 4");
            playerCount = 4;
        }
        

        switch (playerCount)
        {
            case 2:
                //set up the board for 2 players.                

                //Create all the walls
                Instantiate(innerWall, Vector3.zero, Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3((gameWidth / 2) - innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;
                Instantiate(innerWall, new Vector3(- (gameWidth / 2) + innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;

                //Create the players
                players.Add(Instantiate(playerPrefab, new Vector3( ((gameWidth / 2) - innerWallWidth / 2) / 2, 0, 0), Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(-((gameWidth / 2) + innerWallWidth / 2) / 2, 0, 0), Quaternion.identity));

                //Set up the controllers
                players[0].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[0].GetComponent<Player>().controller.ControllerID = 0;

                players[1].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[1].GetComponent<Player>().controller.ControllerID = 1;

                //Spawn the gun
                Instantiate(gunPrefab, new Vector3(((gameWidth / 2) - innerWallWidth / 2) / 2, 0, 0) + gunOffset, Quaternion.identity);

                //create the enemy spawners
                Instantiate(enemySpawnerPrefab, new Vector3(((gameWidth / 2) - innerWallWidth / 2) / 2, gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[0];
                Instantiate(enemySpawnerPrefab, new Vector3(-((gameWidth / 2) + innerWallWidth / 2) / 2, gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[1];
                break;
            case 3:
                //set up the board for 3 players.
                gameWidthLessWalls = gameWidth - (innerWallWidth * 2);

                //create all the walls
                Instantiate(innerWall, new Vector3(gameWidthLessWalls / 6, 0, 0), Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3(-gameWidthLessWalls / 6, 0, 0), Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3((gameWidth / 2) - innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;
                Instantiate(innerWall, new Vector3(-(gameWidth / 2) + innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;

                //create the players
                players.Add(Instantiate(playerPrefab, Vector3.zero, Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(gameWidthLessWalls * 1 / 3, 0, 0), Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(gameWidthLessWalls * -1 / 3, 0, 0), Quaternion.identity));

                //set up the controllers
                players[0].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[0].GetComponent<Player>().controller.ControllerID = 0;

                players[1].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[1].GetComponent<Player>().controller.ControllerID = 1;

                players[2].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[2].GetComponent<Player>().controller.ControllerID = 2;

                //spawn the gun
                Instantiate(gunPrefab, Vector3.zero + gunOffset, Quaternion.identity);

                //create the enemy spawners
                Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[0];
                Instantiate(enemySpawnerPrefab, new Vector3(gameWidthLessWalls * 1 / 3, gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[1];
                Instantiate(enemySpawnerPrefab, new Vector3(gameWidthLessWalls * -1 / 3, gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[2];
                break;
            case 4:
                //set up the board for 4 players.
                gameWidthLessWalls = gameWidth - (innerWallWidth * 2);

                //create all the walls
                Instantiate(innerWall, Vector3.zero, Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3(gameWidthLessWalls / 4, 0, 0), Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3(-gameWidthLessWalls / 4, 0, 0), Quaternion.identity).layer = 10;
                Instantiate(innerWall, new Vector3((gameWidth / 2) - innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;
                Instantiate(innerWall, new Vector3(-(gameWidth / 2) + innerWallWidth / 2, 0, 0), Quaternion.identity).layer = 9;

                //Create the players
                players.Add(Instantiate(playerPrefab, new Vector3((innerWallWidth / 2) + (gameWidthLessWalls / 8), 0, 0), Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(-(innerWallWidth / 2) - (gameWidthLessWalls / 8), 0, 0), Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(-3 * (innerWallWidth / 2) - 3 * (gameWidthLessWalls / 8), 0, 0), Quaternion.identity));
                players.Add(Instantiate(playerPrefab, new Vector3(3 * (innerWallWidth / 2) + 3f * (gameWidthLessWalls / 8), 0, 0), Quaternion.identity));

                //set up the controllers
                players[0].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[0].GetComponent<Player>().controller.ControllerID = 0;

                players[1].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[1].GetComponent<Player>().controller.ControllerID = 1;

                players[2].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[2].GetComponent<Player>().controller.ControllerID = 2;

                players[3].GetComponent<Player>().controller = Instantiate(controllerObject, Vector3.zero, Quaternion.identity).GetComponent<Controller>();
                players[3].GetComponent<Player>().controller.ControllerID = 3;

                //spawn the gun
                Instantiate(gunPrefab, new Vector3((innerWallWidth / 2) + (gameWidthLessWalls / 8), 0, 0) + gunOffset, Quaternion.identity);

                //create the enemy spawners
                Instantiate(enemySpawnerPrefab, new Vector3((innerWallWidth / 2) + (gameWidthLessWalls / 8), gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[0];
                Instantiate(enemySpawnerPrefab, new Vector3(-(innerWallWidth / 2) - (gameWidthLessWalls / 8), gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[1];
                Instantiate(enemySpawnerPrefab, new Vector3(-3 * (innerWallWidth / 2) - 3 * (gameWidthLessWalls / 8), gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[2];
                Instantiate(enemySpawnerPrefab, new Vector3(3 * (innerWallWidth / 2) + 3f * (gameWidthLessWalls / 8), gameHeight / 2, 0), Quaternion.identity).GetComponent<Spawner>().EnemyTarget = players[3];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
