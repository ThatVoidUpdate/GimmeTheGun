using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType{Past, Future}

public class PlayerController : MonoBehaviour
{
    public PlayerType Player;
    public float HorizontalSpeed;
    public float JumpForce;

    public Collider2D Ground;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        switch (Player)
        {
            case PlayerType.Past:
                rb.velocity = new Vector2(Input.GetAxis("PastHorizontal") * HorizontalSpeed * Time.deltaTime, rb.velocity.y);
                if (Input.GetButton("PastJump") && rb.IsTouching(Ground))
                {
                    rb.AddForce(new Vector2(0, JumpForce));
                }
                break;
            case PlayerType.Future:
                rb.velocity = new Vector2(Input.GetAxis("FutureHorizontal") * HorizontalSpeed * Time.deltaTime, rb.velocity.y);
                if (Input.GetButton("FutureJump") && rb.IsTouching(Ground))
                {
                    rb.AddForce(new Vector2(0, JumpForce));
                }
                break;
        }
    }
}
