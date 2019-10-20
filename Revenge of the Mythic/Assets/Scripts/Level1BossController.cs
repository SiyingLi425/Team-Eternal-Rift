﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BossController : MeleeEnemy
{
    private int dashTimer = 0, dashTimerReset = 250, chargeUpTimer = 0, chargeUpTimerReset = 50;
    private float speedIncrement = 5, xSpeed, ySpeed;
    private Vector3 dashTarget;

    void FixedUpdate()
    {
        if (dashTimer > 1)
        {
            --dashTimer;
            if (dashTimer <= 0)
            {
                Debug.Log("iello");
                chargeUpTimer = chargeUpTimerReset;
            }
        }
        else if (chargeUpTimer > 0)
        {
            //Pasue all other scripts
            --chargeUpTimer;
            if (chargeUpTimer <= 0)
            {
                dashTarget = AggroedPlayer.transform.position;
                Debug.Log("hello");
                #region Set Speed
                Vector3 t = AggroedPlayer.transform.position;
                float xDif = Mathf.Abs(transform.position.x - t.x), yDif = Mathf.Abs(transform.position.y - t.y);
                bool xIsCloser = xDif > yDif;
                switch (xIsCloser)
                {
                    case true:
                        xSpeed = walkSpeed * speedIncrement;
                        ySpeed = yDif / xDif * walkSpeed * speedIncrement;
                        break;
                    case false:
                        ySpeed = walkSpeed * speedIncrement;
                        xSpeed = xDif / yDif * walkSpeed * speedIncrement;
                        break;
                }
                xSpeed = transform.position.x > t.x ? -xSpeed : xSpeed;
                ySpeed = transform.position.y > t.y ? -ySpeed : ySpeed;
                #endregion
            }
        }
    }

    protected override void attack()
    {
        if (dashTimer > 0)
        {
            base.attack();
        }
    }

    public override void moveEnemy(float speedX, float speedY)
    {
        if (dashTimer > 0)
        {
            base.moveEnemy(speedX, speedY);
        }
        else if (chargeUpTimer == 0)
        {
            base.moveEnemy(xSpeed, ySpeed);
            bool pastX = (transform.position.x > dashTarget.x && xSpeed >= 0) || (transform.position.x < dashTarget.x && xSpeed <= 0);
            bool pastY = (transform.position.y > dashTarget.y && ySpeed >= 0) || (transform.position.y < dashTarget.y && ySpeed <= 0);
            if (pastX && pastY)
            {
                dashTimer = dashTimerReset;
            }
        }
    }

    protected override void getMovementTargert()
    {
        if (dashTimer > 0)
        {
            base.getMovementTargert();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            dashTimer = dashTimerReset;
        }
    }
}