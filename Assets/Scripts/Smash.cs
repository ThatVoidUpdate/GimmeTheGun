using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{

    public Shader SmashShader;
    public int LinesAmount;
    public float MaxSpeed;

    private List<float[]> Lines = new List<float[]>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSmash()
    {
        long StartTicks = System.DateTime.Now.Ticks;
        Sprite CurrentSprite = GetComponent<SpriteRenderer>().sprite;

        //Generate points
        for (int i = 0; i < LinesAmount; i++)
        {
            int p1x = Random.Range(0, CurrentSprite.texture.width);
            int p1y = Random.Range(0, CurrentSprite.texture.height);
            int p2x = Random.Range(0, CurrentSprite.texture.width);
            int p2y = Random.Range(0, CurrentSprite.texture.height);

            float m = (p2y - p1y) / (p2x - p1x);
            float c = p1y - (m * p1x);
            Lines.Add(new float[] { m, c });
        }
        print("Generating points took " + (System.DateTime.Now.Ticks - StartTicks) + " ticks");
        StartTicks = System.DateTime.Now.Ticks;

        //Generate objs
        for (int i = 0; i < Mathf.Pow(2, LinesAmount); i++)
        {
            GameObject particle = new GameObject("Smashed Particle");
            SpriteRenderer rend = particle.AddComponent<SpriteRenderer>();


            rend.sprite = CurrentSprite;

            particle.layer = 8;

            Texture2D texture = new Texture2D(CurrentSprite.texture.width, CurrentSprite.texture.height, TextureFormat.ARGB32, false);


            StartTicks = System.DateTime.Now.Ticks;


            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    bool drawPixel = true;
                    for (int j = 0; j < LinesAmount; j++)
                    {
                        if ((i & (1 << j)) != 0)
                        {
                            if (y > Lines[j][0] * x + Lines[j][1])
                            {
                                drawPixel = false;
                            }
                        }
                        else
                        {
                            if (y < Lines[j][0] * x + Lines[j][1])
                            {
                                drawPixel = false;
                            }
                        }
                    }

                    texture.SetPixel(x, y, drawPixel ? Color.white : Color.clear);
                    
                }
            }
            texture.Apply();
            print("Creating mask texture took " + (System.DateTime.Now.Ticks - StartTicks) + " ticks");
            StartTicks = System.DateTime.Now.Ticks;

            rend.material = new Material(SmashShader);
            rend.material.SetTexture("_MainTex", CurrentSprite.texture);
            rend.material.SetTexture("_Mask", texture);

            Rigidbody2D rb = particle.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.AddForce(new Vector2(Random.Range(-MaxSpeed, MaxSpeed), Random.Range(-MaxSpeed, MaxSpeed)));
            particle.AddComponent<CircleCollider2D>();

            particle.transform.position = transform.position;
        }

        Destroy(this.gameObject);

    }
}
