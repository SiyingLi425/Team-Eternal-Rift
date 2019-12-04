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
        GameObject charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        if (charChoice.GetComponent<CanvasGroup>().alpha == 1f)
        {
            pegasusDesc.SetActive(true);
        }
    }
}
