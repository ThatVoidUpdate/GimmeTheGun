using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Control Options")]
    public bool KeyboardPlayer;
    public int ControllerNumber;

    [Header("Speeds")]
    public float HorizontalSpeed;
    public float VerticalSpeed;

    [Header("Held object options")]
    public GameObject HeldObject;
    public Vector2 HeldOffset;
    private List<Collider2D> colliders = new List<Collider2D>();

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyboardPlayer)
        {
            rb.velocity = new Vector2(Input.GetAxis("KeyboardHorizontal") * HorizontalSpeed, Input.GetAxis("KeyboardVertical") * VerticalSpeed);

            if (Input.GetButton("KeyboardGrab") && colliders.Count > 0)
            {
                colliders[0].transform.position = gameObject.transform.position + new Vector3(HeldOffset.x, HeldOffset.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
}
