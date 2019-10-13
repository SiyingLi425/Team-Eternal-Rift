using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    public float minDistanceAway;
    public GameObject bullet;
    public Transform bulletSpawn;
    public bool TooClose = false;

    protected Collider2D minDistance;

    protected override void Start()
    {
        base.Start();
        
        minDistance = GetComponent<CapsuleCollider2D>();

    }
    protected override void Update()
    {
        base.Update();
        if (playerCollider.IsTouching(minDistance))
        {
            TooClose = true;
        }
        else
        {
            TooClose = false;
        }

        if (TooClose == true)
        {
            if (attackCoolDown == 0)
            {
                attack();
            }

        }
    }


    public override void moveEnemy(float speedX, float speedY)
    {
        if (TooClose == false)
        {
            GetComponent<Rigidbody2D>().position += new Vector2(speedX, speedY);
          
        }
    }

    protected override void attack()
    {
        Debug.Log("AttackRange");
        attackCoolDown = attackSpeed;
        Instantiate(bullet, bulletSpawn.position, rotate());
    }

    protected Quaternion rotate()
    {
        float offset = 90f;
        Vector2 direction = target - (Vector2)enemyTransform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation= Quaternion.Euler(Vector3.forward * ((angle + 180) + offset));
        return rotation;
    }
}

