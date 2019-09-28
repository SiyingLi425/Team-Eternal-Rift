using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : PlayerController
{
    private CircleCollider2D TauntRange;
    # region Ability Cooldown Variables
    private int[] abilityCooldownReset = { 300, 400, 0 };
    protected override int[] AbilityCooldownReset { get { return abilityCooldownReset; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TauntRange = GetComponent<CircleCollider2D>();
        //Only use this to initialize variables
    }

    // Update is called once per frame
    void Update()
    {
        //Leave this empty
    }

    protected override void Attack1() {
        /*
         Ability 1 [Peck] - Melee damage. Adds a bleeding effect on the enemy that drains hp slowly for 3 seconds. 6 second cooldown.
         */
        foreach (string s in damagable)
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
        foreach (string s in damagable)
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
    protected override void Attack3() { }
}
