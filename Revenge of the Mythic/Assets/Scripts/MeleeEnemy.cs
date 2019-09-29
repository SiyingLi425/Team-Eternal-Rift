using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    //Public Variables
    public int health;
    public float walkSpeed;
    public int attackSpeed, attackDamage;




    //Private Variables
    private Collider2D aggroRange, hitBox, playerCollider;
    private GameObject player;
    private Vector2 target, playerPosition;
    private int attackCoolDown;
    private Transform enemyTransform;
    private Transform playerTransform;



    // Start is called before the first frame update
    void Start()
    {
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
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



    }
    protected void moveEnemy()
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
    
    void rotateEnemy()
    {
        float speed = 3f;
        float offset = 90f;
        Vector2 direction = target - (Vector2)enemyTransform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyTransform.rotation = Quaternion.Euler(Vector3.forward * ((angle+180) + offset));
    }
    public int healthE = 20;
    protected void attack()
    {
        //temp
        //player needs to have health variable
        //player.health = player.health - attackDamage;
        healthE = healthE - attackDamage;
        attackCoolDown = attackSpeed;
        Debug.Log("Attack");
        //where do we put about player death? would the player be able to check for 0 hp? 
    }


}

