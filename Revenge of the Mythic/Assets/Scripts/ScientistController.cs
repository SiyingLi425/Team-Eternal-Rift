using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistController : RangedEnemy
{
    public GameObject poisonPotion;

    protected override void attack()
    {
        attackCoolDown = attackSpeed;
        base.enemyHit.Play();
        Instantiate(poisonPotion, playerPosition, rotate());
    }
}
