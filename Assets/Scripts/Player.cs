using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Direction { Up, Down, Right, Left, None}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Control Options")]
    [SerializeField]
    private Controller controller; //The controller that his player is listening to

    [Header("Speeds")]
    [SerializeField]
    public float HorizontalSpeed;
    [SerializeField]
    public float VerticalSpeed; //The ohirzontal and vertical speed of the player

    [Header("Held object options")]
    [SerializeField]
    public Gun HeldObject = null; //The object that the player is currently holding
    [SerializeField]
    public float ThrowSpeed;

    [SerializeField]
    public List<Collider2D> closeGuns = new List<Collider2D>(); //A list of all the guns in the trigger
    private bool CanDrop = true;

    [Header("Controls")]
    [SerializeField]
    public Control MoveHorizontal;
    [SerializeField]
    public Control MoveVertical;
    [SerializeField]
    public Control LookHorizontal;
    [SerializeField]
    public Control LookVertical;
    [SerializeField]
    public Control Grab;
    [SerializeField]
    public Control Shoot; //The controls for each of the respective actions

    [Header("Graphics")]
    [SerializeField]
    public Sprite UpSprite;
    [SerializeField]
    public Sprite DownSprite;
    [SerializeField]
    public Sprite LeftSprite;
    [SerializeField]
    public Sprite RightSprite; //The sprites for each of their respective directions
    private SpriteRenderer rend; //The sprite renderer attached to this gameobject
    private Direction CurrentDirection = Direction.Down; //The direction that the player is currently facing

    [Header("Health")]
    public float MaxHealth = 100; //The maximum health of the player
    public float Health; //The current health of the player
    public bool dead = false; //Whether the player is dead or not
    private float DamageTime; //How long since the enemy started doing damage
    public float DamageSpeed; //How long it takes an enemy to do damage

    [Header("Bark lines")]
    public GameObject BarkBubble; //The text bubble to pop up when a player wants to say something
    public float ShowTime = 1; //Time in seconds to show the line for
    [Space]
    public float DeathLineChance; //Chance to say a line when the player dies
    public string[] DeathLines; //Lines to say when the player dies
    [Space]
    public float HalfHealthLineChance;//Chance to say a line when the player reaches half health
    public string[] HalfHealthLines; //Lines to say when the player reaches half health
    [Space]
    public float NearDeathLineChance;//Chance to say a line when the player is near death
    public string[] NearDeathLines; //Lines to say when the player is near death
    [Space]
    public float GunThrowLineChance;//Chance to say a line when the player throws the gun
    public string[] GunThrowLines; //Lines to say when the player throws the gun
    [Space]
    public float WaveStartLineChance;//Chance to say a line when the player starts a wave
    public string[] WaveStartLines; //Lines to say when the player starts a wave
    [Space]
    public float WaveEndLineChance;//Chance to say a line when the player ends a wave
    public string[] WaveEndLines; //Lines to say when the player ends a wave
    [Space]
    public float EnemyKillLineChance;//Chance to say a line when the player kills an enemy
    public string[] EnemyKillLines; //Lines to say when the player kills an enemy

    private bool InvertedControls = false; // Support for drunk powerup
    private bool CanMove = true; // Support for turret powerup

    private Rigidbody2D rb; //The rigidbody2d attached to this gameobject
    private float theta = 90; //The current angle of the rotation stick

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        rend.sprite = DownSprite;

        Health = MaxHealth;
    }


    void Update()
    {       
        rb.velocity = new Vector2(controller.GetControlState(MoveHorizontal) * (CanMove ? (InvertedControls ? -HorizontalSpeed : HorizontalSpeed) : 0), controller.GetControlState(MoveVertical) * (CanMove ? (InvertedControls ? VerticalSpeed : -VerticalSpeed) : 0)); //Set the movement speed according to the controller input        

        if (controller.GetControlDown(Grab) && closeGuns.Count > 0 && HeldObject == null && !dead)
        {//There is a gun close to us, and we are attempting to grab it, and we arent currently holding a gun, and we arent currently dead
            HeldObject = closeGuns[0].GetComponent<Gun>();
            CanDrop = false;
            if (transform.position.x < 0)
            {
                closeGuns[0].GetComponent<Gun>().OnLeftSideOfScreen = true;
            }
            else
            {
                closeGuns[0].GetComponent<Gun>().OnLeftSideOfScreen = false;
            }
        }

        if (controller.GetControlState(Grab) == 0)
        {//Let the player drop the gun after they release the grab button
            CanDrop = true;
        }

        if (controller.GetControlState(Shoot) == 1 && HeldObject != null)
        {//We are trying to shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = true;
        }
        else if (controller.GetControlState(Shoot) == 0 && HeldObject != null)
        {//We are trying to not shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = false;
        }

        if (controller.GetControlDown(Grab) && HeldObject != null && CanDrop)
        {//We are holding a gun, and trying to drop it
            if (Random.Range(0f, 1f) < GunThrowLineChance)
            {//Show a gun throw line
                StartCoroutine(ShowBarkLine(GunThrowLines[Random.Range(0, GunThrowLines.Length - 1)], ShowTime));
            }

            HeldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowSpeed * Mathf.Cos(theta * 2 * Mathf.PI / 360), ThrowSpeed * Mathf.Sin(theta * 2 * Mathf.PI / 360));
            HeldObject = null;
        }

        if (HeldObject != null)
        {//We are holding a gun, so move it around the player
            HeldObject.SetAngle(theta, transform.position);

        }

        //Maths to get the angle of the stick assigned to rotation
        if (controller.GetControlState(LookVertical) != 0 || controller.GetControlState(LookHorizontal) != 0)
        {
            if (Mathf.Atan(controller.GetControlState(LookVertical) / controller.GetControlState(LookHorizontal)) / Mathf.PI == 0.5)
            {
                theta = - Mathf.PI / 2;
            }
            else if (Mathf.Atan(controller.GetControlState(LookVertical) / controller.GetControlState(LookHorizontal)) / Mathf.PI == -0.5)
            {
                theta = Mathf.PI / 2;
            }
            else
            {
                theta = Mathf.Atan(controller.GetControlState(LookVertical) / controller.GetControlState(LookHorizontal));
            }
            

            if (controller.GetControlState(LookHorizontal) > 0)
            {
                 theta += Mathf.PI;
            }

            theta += Mathf.PI;
            theta = theta * -360 / (2 * Mathf.PI);

            
        }

        //Set the current sprite to the correct one for the looking angle
        if (theta < -315 && theta > -405 && CurrentDirection != Direction.Left)
        {
            CurrentDirection = Direction.Left;
            rend.sprite = RightSprite;
        }
        else if (theta < -225 && theta > -315 && CurrentDirection != Direction.Up)
        {
            CurrentDirection = Direction.Up;
            rend.sprite = UpSprite;
        }
        else if (theta < -135 && theta > -225 && CurrentDirection != Direction.Right)
        {
            CurrentDirection = Direction.Right;
            rend.sprite = LeftSprite;
        }
        else if ((theta > -135 || theta < -405) && CurrentDirection != Direction.Down)
        {
            CurrentDirection = Direction.Down;
            rend.sprite = DownSprite;
        }
    }


    public void TakeDamage(float DamageAmount)
    {
        //If we are currently above half health, but taking damage would take us below half health
        if (Health > MaxHealth / 2 && Health - DamageAmount <= MaxHealth / 2)
        {//Say a half health line            
            if (Random.Range(0f, 1f) < HalfHealthLineChance)
            {
                StartCoroutine(ShowBarkLine(HalfHealthLines[Random.Range(0, HalfHealthLines.Length-1)], ShowTime));
            }
        }

        //If we are currently above 10% health, but taking damage would take us below 10% health
        if (Health > MaxHealth / 10 && Health - DamageAmount <= MaxHealth / 10)
        {//Say a near death line            
            if (Random.Range(0f, 1f) < NearDeathLineChance)
            {
                StartCoroutine(ShowBarkLine(NearDeathLines[Random.Range(0, NearDeathLines.Length-1)], ShowTime));
            }
        }


        Health -= DamageAmount;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (Random.Range(0f,1f) < DeathLineChance)
        {
            StartCoroutine(ShowBarkLine(DeathLines[Random.Range(0,DeathLines.Length-1)], ShowTime));
        }
        
        rend.color = new Color(1, 1, 1, 0.5f);
        dead = true;
        HeldObject = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageTime = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                DamageTime += Time.deltaTime;
            }

            if (DamageTime > DamageSpeed)
            {
                DamageTime = 0;
                TakeDamage(collision.gameObject.GetComponent<Enemy>().Damage);
            }
        }
    }

    IEnumerator ShowBarkLine(string Line, float WaitTime)
    {
        BarkBubble.SetActive(true);
        BarkBubble.GetComponentInChildren<TextMeshProUGUI>().text = Line;
        BarkBubble.GetComponent<BarkBubble>().TrackingObject = gameObject;
        yield return new WaitForSeconds(WaitTime);
        BarkBubble.SetActive(false);
    }

    public void EndWave()
    {
        if (Random.Range(0f, 1f) < WaveStartLineChance)
        {
            StartCoroutine(ShowBarkLine(WaveStartLines[Random.Range(0, WaveStartLines.Length - 1)], ShowTime));
        }
    }

    public void StartWave()
    {
        if (Random.Range(0f, 1f) < WaveEndLineChance)
        {
            StartCoroutine(ShowBarkLine(WaveEndLines[Random.Range(0, WaveEndLines.Length - 1)], ShowTime));
        }
    }

    public void KillEnemy()
    {
        if (Random.Range(0f, 1f) < EnemyKillLineChance)
        {
            StartCoroutine(ShowBarkLine(EnemyKillLines[Random.Range(0, EnemyKillLines.Length - 1)], ShowTime));
        }
    }

    /// <summary>
    /// Returns the controller attached to the player
    /// </summary>
    /// <returns>The local controller</returns>
    public Controller GetController()
    {
        return controller;
    }

    /// <summary>
    /// Sets the local controller
    /// </summary>
    /// <param name="_Controller">The controller to set to</param>
    public void SetController(Controller _Controller)
    {
        controller = _Controller;
    }

    /// <summary>
    /// Sets the id of the controller attached to the player
    /// </summary>
    /// <param name="ID">The ID to set it to</param>
    public void SetControllerID(int ID)
    {
        controller.SetControllerID(ID);
    }

    public void SetControlInversion(bool Inverted)
    {
        InvertedControls = Inverted;
    }

    public void SetCanMove(bool _CanMove)
    {
        CanMove = _CanMove;
    }
}

//#7582 ♥
