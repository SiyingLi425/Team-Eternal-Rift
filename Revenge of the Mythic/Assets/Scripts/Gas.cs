using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : ItemController
{
    protected override void effect(PlayerController p)
    {
        p.Damage(0, "Slow");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
