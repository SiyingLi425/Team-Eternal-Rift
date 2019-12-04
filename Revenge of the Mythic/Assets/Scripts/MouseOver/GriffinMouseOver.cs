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
        timer = 200;
    }

    public override void OnMouseExit()
    {
        griffinDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        GameObject charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        if (charChoice.GetComponent<CanvasGroup>().alpha == 1f && canDisplay == true)
        {
            griffinDesc.SetActive(true);
        }

    }
}

