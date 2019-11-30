//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using UnityEngine;

public abstract class OnMouseOverController : MonoBehaviour
{

    public abstract void OnMouseOver();
    public abstract void OnMouseExit();
}
