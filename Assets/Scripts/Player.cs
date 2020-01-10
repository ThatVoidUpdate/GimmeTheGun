using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Right, Left}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Control Options")]
    public Controller controller; //The controller that his player is listening to

    [Header("Speeds")]
    public float HorizontalSpeed;
    public float VerticalSpeed; //The ohirzontal and vertical speed of the player

    [Header("Held object options")]
    public GameObject HeldObject = null; //The object that the player is currently holding
    public float HeldDistance; //The distance to hold the object at
    public float ThrowSpeed;

    public List<Collider2D> closeGuns = new List<Collider2D>(); //A list of all the guns in the trigger
    private bool CanDrop = true;

    [Header("Controls")]
    public Control MoveHorizontal;
    public Control MoveVertical;
    public Control LookHorizontal;
    public Control LookVertical;
    public Control Grab;
    public Control Shoot; //The controls for each of the respective actions

    [Header("Graphics")]
    public Sprite UpSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite; //The sprites for each of their respective directions

    private Rigidbody2D rb;
    public float theta = 90; //The current angle of the rotation stick

    private SpriteRenderer rend;
    private Direction CurrentDirection = Direction.Down;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        rend.sprite = DownSprite;

        /*Select the correct image from the resources folder, depeding on the player graphic selected
        switch (Graphics)
        {//Select the correct iamge freom the resources folder, and load it
            case PlayerGraphics.Blue:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Blue");
                break;
            case PlayerGraphics.Green:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Green");
                break;
            case PlayerGraphics.Orange:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Orange");
                break;
            case PlayerGraphics.Pink:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Pink");
                break;
            case PlayerGraphics.Purple:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Purple");
                break;
            case PlayerGraphics.Yellow:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Yellow");
                break;
            default:
                break;
        }*/
    }


    void Update()
    {       
        rb.velocity = new Vector2(controller.ControlState[MoveHorizontal] * HorizontalSpeed, controller.ControlState[MoveVertical] * -VerticalSpeed); //Set the movement speed according to the controller input        

        if (controller.GetControllerDown(Grab) && closeGuns.Count > 0 && HeldObject == null)
        {//There is a gun close to us, and we are attempting to grab it, and we arent currently holding a gun
            HeldObject = closeGuns[0].gameObject;
            CanDrop = false;
        }

        if (controller.ControlState[Grab] == 0)
        {//Let the player drop the gun after they release the grab button
            CanDrop = true;
        }

        if (controller.ControlState[Shoot] == 1 && HeldObject != null)
        {//We are trying to shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = true;
        }
        else if (controller.ControlState[Shoot] == 0 && HeldObject != null)
        {//We are trying to not shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = false;
        }

        if (controller.GetControllerDown(Grab) && HeldObject != null && CanDrop)
        {//We are holding a gun, and trying to drop it
            HeldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowSpeed * Mathf.Cos(theta * 2 * Mathf.PI / 360), ThrowSpeed * Mathf.Sin(theta * 2 * Mathf.PI / 360));
            HeldObject = null;
        }

        if (HeldObject != null)
        {//We are holding a gun, so move it around the player
            HeldObject.transform.position = new Vector2(transform.position.x + (HeldDistance * Mathf.Cos(theta * 2 * Mathf.PI / 360)), transform.position.y + (HeldDistance * Mathf.Sin(theta * 2 * Mathf.PI / 360)));
            HeldObject.transform.eulerAngles = new Vector3(0, 0, theta - 90);
        }

        //Maths to get the angle of the stick assigned to rotation
        if (controller.ControlState[LookVertical] != 0 || controller.ControlState[LookHorizontal] != 0)
        {
            if (Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]) / Mathf.PI == 0.5)
            {
                theta = - Mathf.PI / 2;
            }
            else if (Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]) / Mathf.PI == -0.5)
            {
                theta = Mathf.PI / 2;
            }
            else
            {
                theta = Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]);
            }
            

            if (controller.ControlState[LookHorizontal] > 0)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Whenever an object enters the trigger around the player, and it has the tag Gun and isnt in the colliders list, add it to the colliders list
        if (!closeGuns.Contains(other) && other.gameObject.CompareTag("Gun")) 
        { 
            closeGuns.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //If a gun move out of the trigger, stop it from shooting and being held, and remove it from the colliders list
        if (other.gameObject.CompareTag("Gun"))
        {
            //closeGuns.Find(x => x == other).GetComponent<Gun>().held = false;
            closeGuns.Find(x => x == other).GetComponent<Gun>().Shooting = false;

            closeGuns.Remove(other);
        }        
    }
}
