using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItemController : ItemController
{
    public int healPercentage;
    protected override void effect()
    {
        playerController.Heal(healPercentage);
    }
}
