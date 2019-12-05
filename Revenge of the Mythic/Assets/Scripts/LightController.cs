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
