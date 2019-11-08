using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : PlayerController
{
    [SerializeField]
    private CircleCollider2D TauntRange;

    protected override void Attack1() {
        //Ability 1 [Peck] - Melee damage. Adds a bleeding effect on the enemy that drains hp slowly for 3 seconds. 6 second cooldown.
        Debug.Log("Peck");
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && BasicAttackRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(2, "Bleed");
                }
            }
        }
    }
    protected override void Attack2() {
        //Ability 2 [Battle Cry] - Effect.Grab Agro of nearby enemies. 8 seconds cooldown.
        foreach (string s in Damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && TauntRange.IsTouching(g.PrimaryCollider()))
                {
                    g.PrimaryController().Damage(0, "Taunt");
                }
            }
        }
    }
    protected override void Attack3() {
        //Ability 3 [Shield] - Defence. Nullify attacks that hit the shield. Lasts for 5 seconds. 10 second cooldown.
        aegisTimer = aegisTimerReset;
    }
}
