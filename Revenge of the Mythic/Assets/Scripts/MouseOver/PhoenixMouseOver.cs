using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixMouseOver : OnMouseOverController
{
    protected GameObject phoenixDesc;
    public void Start()
    {


        phoenixDesc = GameObject.FindGameObjectWithTag("PhoenixDesc");
        phoenixDesc.SetActive(false);
    }
    public override void OnMouseExit()
    {
        phoenixDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        phoenixDesc.SetActive(true);
    }
}
