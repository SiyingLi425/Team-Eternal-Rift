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
        GameObject charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        if (charChoice.GetComponent<CanvasGroup>().alpha == 1f)
        {
            dragonDesc.SetActive(true);
        }
    }
}
