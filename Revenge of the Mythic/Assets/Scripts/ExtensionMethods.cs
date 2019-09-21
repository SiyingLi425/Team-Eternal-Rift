using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Damage(this GameObject g, int i) {
        /*
         This should work if left blank; this is the lowest scope possible
         Each object that can be damaged will likely have its own unique damage method
         If this is not the case, add type conversion and then write out all the things here
         */
    }

    public static Collider2D PrimaryCollider(this GameObject g) {
        //Add type conversion and a switch statement
        return new Collider2D() { enabled = false};
    }
}
