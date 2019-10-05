using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{

    //Public Variables
    public int health;
    public float walkSpeed;
    public int attackSpeed, attackDamage;
    




    //Private Variables
    
    protected Collider2D aggroRange, hitBox, playerCollider;
    private GameObject player;
    private PlayerController playerController;
    protected Vector2 target, playerPosition;
    protected int attackCoolDown;
    private Transform enemyTransform;
    private Transform playerTransform;
    

  

    private string status;


    [Header("Status Effects")]
    public float bleedTimer;
    public float bleedTime;

    public Collider2D HitBox { get { return hitBox; } }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        playerPosition = player.GetComponent<Transform>().position;


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (playerCollider.IsTouching(hitBox) && attackCoolDown == 0)
        {

            attack();
        }

        if (playerCollider.IsTouching(aggroRange))
        {
            target = playerPosition;
            moveEnemy();
        }

        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }

        if (bleedTimer > 0 && bleedTime % (25/100 * bleedTimer) == 0 )
        {  
            playerController.Damage(1);
         
        }

        if(bleedTimer > 0)
        {
             bleedTimer--;
        }
       

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.PrimaryCollider().IsTouching(HitBox))
            {
                playerController = p.GetComponent<PlayerController>();

            }
        }
    }
    protected virtual void  moveEnemy()
    {
 
        float speedX = 0, speedY = 0;
        Vector2 pos = GetComponent<Transform>().position;
        float xDistance = Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(target.x));
        float yDistance = Mathf.Abs(Mathf.Abs(pos.y) - Mathf.Abs(target.y));

        if (xDistance > yDistance)
        {
            speedX = walkSpeed;
            speedY = yDistance / (xDistance / walkSpeed);
        }
        else
        {
            speedY = walkSpeed;
            speedX = xDistance / (yDistance / walkSpeed);
        }

        if (target.x < pos.x)
        {
            speedX = -speedX;
        }

        if (target.y < pos.y)
        {
            speedY = -speedY;
        }

        GetComponent<Rigidbody2D>().position += new Vector2(speedX, speedY);
        rotateEnemy();

    }

    protected void rotateEnemy()
    {
        float offset = 90f;
        Vector2 direction = target - (Vector2)enemyTransform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyTransform.rotation = Quaternion.Euler(Vector3.forward * ((angle + 180) + offset));
    }

    protected virtual void attack()
    {
        playerController.Damage(attackDamage);
        attackCoolDown = attackSpeed;
    }

    public virtual void Damage(int attackDamage)
    {
        health -= attackDamage;
    }

    public virtual void Damage(int attackDamage, string s)
    {
        health -= attackDamage;
        status = s;

       
 

        if (status == "Bleed" || status == "Burn")
        {
            bleedTimer = bleedTime;
        }
        if(status == "Taunt")
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<CircleCollider2D>().IsTouching(HitBox))
                {
                    playerController = p.GetComponent<PlayerController>();

                }
            }
            target = player.GetComponent<Transform>().position;
        }

    }


}

