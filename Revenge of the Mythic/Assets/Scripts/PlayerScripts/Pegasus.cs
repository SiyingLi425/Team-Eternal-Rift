using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : PlayerController
{
    [SerializeField]
    private PolygonCollider2D wingBuffetRange;
    [SerializeField]
    private BoxCollider2D dashPath;
    [SerializeField]
    private Transform dashLocation;

    #region Sound Public Variables
    [Header("Audio Controller")]
    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    #endregion
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
        attackSound1.Play();
    }
    protected override void Attack2()
    {
        //Ability 2 [Dash] - A short dash that damages everything in your path. 8 seconds cooldown.
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
        attackSound2.Play();
    }
    protected override void Attack3()
    {
        //Ability 3 [Wing Buffet] - Ranged damage cone. Knocks back. Does more damage versus bosses(instead of using knock back). 10 second cooldown.
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
        attackSound3.Play();
    }
}
