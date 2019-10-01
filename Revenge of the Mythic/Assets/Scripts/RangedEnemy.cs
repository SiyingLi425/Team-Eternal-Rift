using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    public float minDistanceAway;
    public GameObject bullet;
    public Transform bulletSpawn;

    protected override void Start()
    {
        base.Start();
        //bullet = GameObject.FindGameObjectWithTag("Bullet");
    }
    protected override void moveEnemy()
    {
        float speedX = 0, speedY = 0;
        Vector2 pos = GetComponent<Transform>().position;
        float xDistance = Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(target.x));
        float yDistance = Mathf.Abs(Mathf.Abs(pos.y) - Mathf.Abs(target.y));
        float avgDistance = xDistance + yDistance / 2;

        //if (xDistance > yDistance)
        //{
        //    avgDistance = xDistance;
        //}
        //else
        //{
        //    avgDistance = yDistance;
        //}
        if (avgDistance > minDistanceAway)
        {
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
            base.rotateEnemy();
        }
        //else if (attackCoolDown == 0 && biggerDistance <= minDistanceAway)
        //{
        //    attack();
        //}
    }

    protected override void attack()
    {
        Debug.Log("AttackRange");
        attackCoolDown = attackSpeed;
        Instantiate(bullet, bulletSpawn.position, this.transform.rotation);
    }
}

