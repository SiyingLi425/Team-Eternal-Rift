using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffinMouseOver : OnMouseOverController
{
    protected GameObject griffinDesc;

    public void Start()
    {
        griffinDesc = GameObject.FindGameObjectWithTag("GriffinDesc");
        griffinDesc.SetActive(false);
    }

    public override void OnMouseExit()
    {
        griffinDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        griffinDesc.SetActive(true);
    }
}

