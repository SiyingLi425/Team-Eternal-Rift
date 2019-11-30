using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMouseOver : OnMouseOverController
{

    protected GameObject dragonDesc;

    public void Start()
    {
        dragonDesc = GameObject.FindGameObjectWithTag("DragonDesc");
        dragonDesc.SetActive(false);

    }
    public override void OnMouseExit()
    {
        dragonDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        dragonDesc.SetActive(true);
    }
}
