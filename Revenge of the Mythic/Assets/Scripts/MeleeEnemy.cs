using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public override void moveEnemy(float speedX, float speedY)
    {
        GetComponent<Rigidbody2D>().position += new Vector2(speedX, speedY);
    }

}

