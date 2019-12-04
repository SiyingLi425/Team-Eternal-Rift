//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using UnityEngine;

public abstract class OnMouseOverController : MonoBehaviour
{
    protected GameObject charChoice;
    protected int timer;
    protected bool canDisplay = false;

    public void Update()
    {
        if(timer > 0)
        {
            timer--;
        }
        if(timer == 0)
        {
            canDisplay = true;
        }
    }
    public abstract void OnMouseOver();
    public abstract void OnMouseExit();
}
