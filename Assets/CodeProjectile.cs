using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeProjectile : MonoBehaviour
{
    private TextMeshProUGUI text;
    public int Characters;
    public float TextSpeed;

    public float Damage;

    public Vector3 Movement;
    public float Speed;

    private float currentTime;
    private string AllowedCharacters = "abcdefghijklmnopqrstuvwxyz1234567890<>,./?-=();";
    private string displayText;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 1 / TextSpeed)
        {
            currentTime = 0;

            displayText = "";

            for (int i = 0; i < Characters; i++)
            {
                displayText += AllowedCharacters[Random.Range(0, AllowedCharacters.Length)];
            }

            text.text = displayText;
        }

        transform.position += Movement * Speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {//we hit a player, do some damage to them
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
