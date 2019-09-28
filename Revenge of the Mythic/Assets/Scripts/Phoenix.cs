using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : PlayerController
{
    public GameObject Fireball, Erupt;
    public Transform FireballSpawn, EruptSpawn;
    #region Ability Cooldown Variables
    private int[] abilityCooldownReset = { 0, 0, 0 };
    protected override int[] AbilityCooldownReset { get { return abilityCooldownReset; } }
    #endregion

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
        //Ability 1 [Fireball] - Fireball. Does more damage if it passes through ‘Inferno Barrier’. 2 seconds cooldown.
        Instantiate(Fireball, FireballSpawn.position, FireballSpawn.rotation);
    }
    protected override void Attack2() {
        //Ability 2 [Erupt] - Ranged AoE.Does damage and applies burning DoT(same as DoT mentioned above). 6 second cooldown.
        Instantiate(Erupt, EruptSpawn.position, EruptSpawn.rotation);
    }
    protected override void Attack3() { }
}
