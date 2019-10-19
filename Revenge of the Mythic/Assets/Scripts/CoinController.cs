using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : ItemController
{
    public int scoreValue;
    // Start is called before the first frame update
    protected override void effect(PlayerController p)
    {
        gameController.AddScore(scoreValue);
        Destroy(this.gameObject);
    }
}
