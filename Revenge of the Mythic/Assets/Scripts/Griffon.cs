using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : PlayerController
{
    //Ability Cooldown Variables
    private int[] abilityCooldownReset = { 400, 0, 0 };
    protected override int[] AbilityCooldownReset { get { return abilityCooldownReset; } }

    // Start is called before the first frame update
    void Start()
    {
        //Leave this empty
    }

    // Update is called once per frame
    void Update()
    {
        //Leave this empty
    }

    protected override void Attack1() {
        /*
         Ability 1 [Peck] - Melee damage. Adds a bleeding effect on the enemy that drains hp slowly for 3 seconds. 8 second cooldown.
         */
        foreach (string s in damagable)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
            {
                if (g.PrimaryCollider().enabled && BasicAttackRange.IsTouching(g.PrimaryCollider()))
                {
                    g.Damage(2);
                    //TODO - figure out how to add a bleed
                }
            }
        }
    }
    protected override void Attack2() { }
    protected override void Attack3() { }
}
