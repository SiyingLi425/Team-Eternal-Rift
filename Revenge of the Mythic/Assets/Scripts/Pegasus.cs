using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : PlayerController
{
    [SerializeField]
    private PolygonCollider2D wingBuffetRange, dashPath;
    [SerializeField]
    private Transform dashLocation;
    protected override void Attack1()
    {
        //Ability 1 [Kick] - Melee damage. Turns around and kicks. Interrupts enemy attack. 6 seconds cool down.
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && BasicAttackRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(3, "Knockback");
                }
            }
        }
    }
    protected override void Attack2()
    {
        //Ability 2 [Dash] - A short dash that damages everything in your path. 8 seconds cooldown.
        #region Rotate Dash Colliders
        {
            float dashOffset = 1.5f, dashRadius = 0.2f, baseLoc = 0.1f;
            switch (Direction)
            {
                case 0:
                    dashLocation.position = new Vector2(0, dashOffset);
                    dashPath.points[0] = new Vector2(-dashRadius, dashOffset);
                    dashPath.points[1] = new Vector2(-dashRadius, baseLoc);
                    dashPath.points[2] = new Vector2(dashRadius, baseLoc);
                    dashPath.points[3] = new Vector2(dashRadius, dashOffset);
                    break;
                case 1:
                    dashLocation.position = new Vector2(dashOffset, 0);
                    dashPath.points[0] = new Vector2(dashOffset, -dashRadius);
                    dashPath.points[1] = new Vector2(baseLoc, -dashRadius);
                    dashPath.points[2] = new Vector2(baseLoc, dashRadius);
                    dashPath.points[3] = new Vector2(dashOffset, dashRadius);
                    break;
                case 2:
                    dashLocation.position = new Vector2(0, -dashOffset);
                    dashPath.points[0] = new Vector2(-dashRadius, -dashOffset);
                    dashPath.points[1] = new Vector2(-dashRadius, -baseLoc);
                    dashPath.points[2] = new Vector2(dashRadius, -baseLoc);
                    dashPath.points[3] = new Vector2(dashRadius, -dashOffset);
                    break;
                case 3:
                    dashLocation.position = new Vector2(-dashOffset, 0);
                    dashPath.points[0] = new Vector2(-dashOffset, -dashRadius);
                    dashPath.points[1] = new Vector2(-baseLoc, -dashRadius);
                    dashPath.points[2] = new Vector2(-baseLoc, dashRadius);
                    dashPath.points[3] = new Vector2(-dashOffset, dashRadius);
                    break;
            }
        }
        #endregion
        #region Wall Check
        bool stop = false;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if (dashPath.IsTouching(g.PrimaryCollider()) || g.PrimaryCollider().bounds.Contains(dashLocation.position))
            {
                stop = true;
                break;
            }
        }
        #endregion
        #region Dash
        if (stop == false)
        {
            foreach (string s in Damagable)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
                {
                    if (g.PrimaryCollider().enabled && dashPath.IsTouching(g.PrimaryCollider()))
                    {
                        g.PrimaryController().Damage(3);
                    }
                }
            }
            rBody.position = dashLocation.position;
        }
        #endregion
    }
    protected override void Attack3()
    {
        //Ability 3 [Wing Buffet] - Ranged damage cone. Knocks back. Does more damage versus bosses(instead of using knock back). 10 second cooldown.
        #region Set Collider Rotation
        switch (Direction)
        {
            case 0:
                wingBuffetRange.points[0] = new Vector2(0, 0.1f);
                wingBuffetRange.points[1] = new Vector2(0.4f, 0.65f);
                wingBuffetRange.points[2] = new Vector2(-0.4f, 0.65f);
                break;
            case 1:
                wingBuffetRange.points[0] = new Vector2(0.1f, 0);
                wingBuffetRange.points[1] = new Vector2(0.65f, 0.4f);
                wingBuffetRange.points[2] = new Vector2(0.65f, -0.4f);
                break;
            case 2:
                wingBuffetRange.points[0] = new Vector2(0, -0.1f);
                wingBuffetRange.points[1] = new Vector2(0.4f, -0.65f);
                wingBuffetRange.points[2] = new Vector2(-0.4f, -0.65f);
                break;
            case 3:
                wingBuffetRange.points[0] = new Vector2(-0.1f, 0);
                wingBuffetRange.points[1] = new Vector2(-0.65f, 0.4f);
                wingBuffetRange.points[2] = new Vector2(-0.65f, -0.4f);
                break;
        }
        #endregion
        #region Wing Buffet
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && wingBuffetRange.IsTouching(g.PrimaryCollider()))
                {
                    int i = g.GetComponent<Level1BossController>() ? 8 : 5;
                    g.PrimaryController().Damage(i, "Knockback");
                }
            }
        }
        #endregion
    }
}
