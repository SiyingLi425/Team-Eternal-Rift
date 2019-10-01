using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Damage(this MonoBehaviour m, int i) {
        /*
         This should work if left blank; this is the lowest scope possible
         Each object that can be damaged will likely have its own unique damage method
         If this is not the case, add type conversion and then write out all the things here
         */
    }
    public static void Damage(this MonoBehaviour m, int i, string s)
    {
        /*
         Same as above, but takes DoTs into account
         */
    }

    public static MonoBehaviour PrimaryController(this GameObject g) {
        switch (g.tag)
        {
            case "Player":
                return g.GetComponent<PlayerController>();
        }
        return null;
    }

    public static Collider2D PrimaryCollider(this GameObject g) {
        switch (g.tag) {
            case "Player":
                return g.GetComponent<PlayerController>().PlayerCollider;
        }

        return new Collider2D() { enabled = false};
    }

    public static int[] Expand(this int[] o, int value) {
        int[] temp = o;
        o = new int[temp.Length + 1];
        for (int z=0; z<temp.Length; ++z)
        {
            o[z] = temp[z];
        }
        o[temp.Length] = value;
        return o;
    }

    public static int[,] Expand(this int[,] o, int[] value, int i)
    {
        int[,] temp = o;
        o = new int[temp.Length + 1, i];
        for (int z = 0; z < temp.Length; ++z)
        {
            for (int y=0; y<i; ++y)
            {
                o[z,y] = temp[z,y];
            }
        }
        for (int z=0;z<i; ++z)
        {
            o[temp.Length,z] = value[z];
        }
        return o;
    }
}
