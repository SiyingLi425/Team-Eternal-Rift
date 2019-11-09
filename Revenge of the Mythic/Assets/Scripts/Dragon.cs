using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : PlayerController
{
    [SerializeField]
    private PolygonCollider2D conflagrationRange;
    [SerializeField]
    private CircleCollider2D roarRange;
    [SerializeField]
    private SpriteRenderer conflagrationSprite;
    private int conflagrationSpriteTimer = 0, conflagrationSpriteTimerReset = 50;

    public override void Update()
    {
        base.Update();
        if (conflagrationSpriteTimer > 0)
        {
            --conflagrationSpriteTimer;
            if (conflagrationSpriteTimer <= 0)
            {
                conflagrationSprite.enabled = false;
            }
        }
    }
    protected override void Attack1()
    {
        //Ability 1 [Nullity Fang] - Melee damage. Silences target for 1 second. Against bosses, this attack interrupts a magic attack instead. 5 second cooldown.
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && BasicAttackRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(4, "Silence");
                }
            }
        }
    }
    protected override void Attack2()
    {
        //Ability 2 [Conflagration] - Ranged damage cone, applies burning DoT for 3 seconds.Leaves burning DoT AoE for 3 seconds[burns tick every 0.5 seconds and stack. They do little damage]. 12 second cooldown.
        conflagrationSprite.enabled = true;
        conflagrationSpriteTimer = conflagrationSpriteTimerReset;
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && conflagrationRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(2, "Burn"); //May change 2 to a number between 1 and 3
                }
            }
        }
    }
    protected override void Attack3()
    {
        //Ability 3[Roar] - Melee AoE fear. Against bosses, this attack only interrupts. 18 second cooldown.
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && roarRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(0, "Fear");
                }
            }
        }
    }
}
