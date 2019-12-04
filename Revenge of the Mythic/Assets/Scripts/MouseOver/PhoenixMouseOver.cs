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
        timer = 200;
    }
    public override void OnMouseExit()
    {
        phoenixDesc.SetActive(false);
    }

    public override void OnMouseOver()
    {
        GameObject charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        if (charChoice.GetComponent<CanvasGroup>().alpha == 1f && canDisplay == true)
        {
            phoenixDesc.SetActive(true);
        }
    }
}
