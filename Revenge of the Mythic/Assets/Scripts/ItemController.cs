using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    public Collider2D collider, playerCollider;
    public GameObject player;
   
    // Start is called before the first frame update
    protected virtual void  Start()
    {
        collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (playerCollider.IsTouching(collider))
           {

            effect();
            Destroy(this.gameObject);
        }
    }

    protected abstract void effect();
}
