using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegasusMouseOver : OnMouseOverController
{

    protected GameObject pegasusDesc;

    public void Start()
    {

        pegasusDesc = GameObject.FindGameObjectWithTag("PegasusDesc");
        pegasusDesc.SetActive(false);


    }

    public override void OnMouseExit()
    {
        pegasusDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        pegasusDesc.SetActive(true);
    }
}
