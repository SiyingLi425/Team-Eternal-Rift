using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGem : ItemController
{
    protected override void effect()
    {
        playerController.Heal(100);
    }
}
