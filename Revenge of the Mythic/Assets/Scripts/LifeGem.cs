using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGem : ItemController
{
    protected override void effect(PlayerController p)
    {
        p.Heal(100);
    }
}
