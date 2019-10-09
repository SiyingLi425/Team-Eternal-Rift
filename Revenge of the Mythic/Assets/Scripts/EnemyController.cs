﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{

    //Public Variables
    public int health;
    public float walkSpeed;
    public int attackSpeed, attackDamage;
    public GameObject meat;


    private Animator enemyAnimator;

    //Private Variables


    protected Collider2D aggroRange, hitBox, playerCollider;

    private GameObject player1;
    private GameObject player2;
    private GameObject aggroedPlayer;
    private PlayerController playerController;
    protected Vector2 target, playerPosition;
    protected int attackCoolDown;
    protected Transform enemyTransform;
    private Transform playerTransform;
    

  

    
    public string status;

    [Header("Status Effects")]
    public float bleedTimer;
    public float bleedTime;
    public float burnTimer;
    public float burnTime;
    public float tauntTimer;
    public float tauntTime;

    public Collider2D HitBox { get { return hitBox; } }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        enemyAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        aggroedPlayer = GameObject.FindGameObjectWithTag("Player");
        playerCollider = aggroedPlayer.GetComponent<BoxCollider2D>();
        if (status != "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (aggroRange.IsTouching(playerCollider))
                {
                    aggroedPlayer = p;
                    playerController = p.GetComponent<PlayerController>();
                }
            }
        }
        
        playerPosition = aggroedPlayer.GetComponent<Transform>().position;
        playerTransform = aggroedPlayer.GetComponent<Transform>();


        if (health <= 0)
        {
            Instantiate(meat, enemyTransform.position, enemyTransform.rotation);
            Destroy(this.gameObject);
        }

        if (playerCollider.IsTouching(hitBox) && attackCoolDown == 0)
        {

            attack();
        }

        if (playerCollider.IsTouching(aggroRange))
        {
            target = playerPosition;
            getMovementTargert();
        }

        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }

        if (bleedTimer > 0 && bleedTime % (25 / 100 * bleedTimer) == 0)
        {
            playerController.Damage(1);

        }

        if (bleedTimer > 0)
        {
            bleedTimer--;
        }
        if (burnTimer > 0)
        {
            burnTimer--;
        }

        if (tauntTimer > 0)
        {
            tauntTime--;
        }

        if (tauntTimer <= 0)
        {

        }
    }
    protected virtual void  getMovementTargert()
    {
 
        float speedX = 0, speedY = 0;
        Vector2 pos = GetComponent<Transform>().position;
        float xDistance = Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(target.x));
        float yDistance = Mathf.Abs(Mathf.Abs(pos.y) - Mathf.Abs(target.y));


        if (xDistance > yDistance)
        {
            speedX = walkSpeed;
            speedY = yDistance / (xDistance / walkSpeed);
            enemyAnimator.SetInteger("Direction", 3);
        
        }
        else
        {
            speedY = walkSpeed;
            speedX = xDistance / (yDistance / walkSpeed);
            enemyAnimator.SetInteger("Direction", 2);
        }

        if (target.x < pos.x)
        {
            speedX = -speedX;
            enemyAnimator.SetInteger("Direction", 1);
        }

        if (target.y < pos.y)
        {
            speedY = -speedY;
            enemyAnimator.SetInteger("Direction", 0);
        }

        moveEnemy(speedX, speedY);

    }

    public abstract void moveEnemy(float speedX, float speedY);

    protected virtual void attack()
    {
        playerController.Damage(attackDamage);
        attackCoolDown = attackSpeed;
    }

    public virtual void Damage(int attackDamage)
    {
        Debug.Log("Getting Damaged 1");
        health -= attackDamage;
    }

    public virtual void Damage(int attackDamage, string s)
    {
        Debug.Log("Getting Damaged 2");
        health -= attackDamage;
        status = s; 

        if (status == "Bleed")
        {
            bleedTimer = bleedTime;
        }
        if(status == "Burn")
        {
            burnTimer = burnTime;
        }

        if(status == "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<CircleCollider2D>().IsTouching(hitBox))
                {
                    aggroedPlayer = p;
                }
            }
            target = aggroedPlayer.GetComponent<Transform>().position;
            tauntTimer = tauntTime;
        }

    }

 


}

