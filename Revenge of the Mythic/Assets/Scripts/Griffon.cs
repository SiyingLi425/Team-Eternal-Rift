using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : PlayerController
{
    //Ability Cooldown Variables
    private int[] abilityCooldownReset = { 0, 0, 0 };
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

    protected override void Attack1() { }
    protected override void Attack2() { }
    protected override void Attack3() { }
}
