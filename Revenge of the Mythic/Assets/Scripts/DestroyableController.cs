using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableController : MonoBehaviour
{
    public void Damage(int i)
    {
        if (i > 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(int i, string s)
    {
        if (i > 0)
        {
            Destroy(gameObject);
        }
    }
}
