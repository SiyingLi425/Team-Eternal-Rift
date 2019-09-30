﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    #region Fields that need to be set inside Unity Editor
    [SerializeField]
    private float speed;
    [SerializeField]
    private int time;
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool destroyOnHit;
    [SerializeField]
    private string[] targets;
    [SerializeField]
    private Collider2D Collider;
    #endregion
    #region Private Variables
    private Rigidbody2D rBody;
    private Vector2 speedVector;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //The following code can be used to make an object always travel in the direction it's facing (according to its z-rotation)
        speedVector = new Vector2(speed * Mathf.Cos(transform.rotation.z*Mathf.Deg2Rad), speed * Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad));
        rBody.position += speedVector;
        --time;
        if (time <= 0)
        {
            Destroy(this);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        bool hit = false;
        foreach (string tar in targets)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(tar))
            {
                if (Collider.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(damage);
                    hit = true;
                }
            }
        }
        if (hit && destroyOnHit)
        {
            Destroy(this);
        }
    }
}
