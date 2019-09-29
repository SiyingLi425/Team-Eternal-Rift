using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
    

    // Start is called before the first frame update
    void Start()
    {
        aggroRange = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerPosition = player.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (playerCollider.IsTouching(hitBox) && attackCoolDown == 0)
        {
            
            attack();
        }

        if (playerCollider.IsTouching(aggroRange) )
        {
            Debug.Log("Aggro");
            target = playerPosition;
             moveEnemy();
        }

        if(attackCoolDown > 0)
        {
            attackCoolDown--;
        }



    }
    protected void moveEnemy()
    {
        Debug.Log("Moving");
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
