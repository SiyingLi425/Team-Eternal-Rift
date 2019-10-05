using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    public Collider2D collider;
   
    // Start is called before the first frame update
    protected virtual void  Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.PrimaryCollider().IsTouching(collider))
            {
                effect(p.GetComponent<PlayerController>());
                Destroy(gameObject);
            }
        }
    }

    protected abstract void effect(PlayerController p);
}
