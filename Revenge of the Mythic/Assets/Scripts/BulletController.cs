﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 speedVector;
    public int time;
    public float speed;
    private Rigidbody2D rBody;
    private GameObject player;
    private PlayerController playerController;
    public int attackDamange;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //The following code can be used to make an object always travel in the direction it's facing (according to its z-rotation)
        //speedVector = new Vector2(speed * Mathf.Cos(transform.rotation.z * Mathf.Deg2Rad), speed * Mathf.Sin(transform.rotation.z * Mathf.Deg2Rad));
        //rBody.position += speedVector;
        rBody.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        --time;

        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerController.Damage(attackDamange);
             Destroy(this.gameObject);
        }

    }
}