using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite on;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CircleCollider2D c = GetComponent<CircleCollider2D>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if (c.IsTouching(g.PrimaryCollider()))
            {
                if (g.transform.position.x == transform.position.x)
                {
                    if (g.transform.position.y > transform.position.y)
                    {
                        transform.position += new Vector3(0, 0.315f);
                        transform.Rotate(0, 0, 270);
                    }
                    else
                    {
                        transform.position += new Vector3(0, -0.315f);
                        transform.Rotate(0, 0, 90);
                    }
                }
                else if (g.transform.position.y == transform.position.y)
                {
                    if (g.transform.position.x > transform.position.x)
                    {
                        if (g.transform.position.x > transform.position.x)
                        {
                            transform.position += new Vector3(0, 0.375f);
                            transform.Rotate(0, 0, 180);
                        }
                        else
                        {
                            transform.position += new Vector3(0, -0.375f);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            sr.sprite = on;
            Destroy(this);
        }
    }

}
