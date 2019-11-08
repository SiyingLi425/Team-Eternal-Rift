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
        if (m is DestroyableController d)
        {
            d.Damage(i);
        }
        else if (m is EnemyController e)
        {
            e.Damage(i);
        }
        else if (m is PlayerController p)
        {
            p.Damage(i);
        }
        else if (m is BulletController b)
        {
            b.gameObject.GetComponent<DestroyableController>().Damage(i);
        }
        else if (m is RangedAttack r)
        {
            r.gameObject.GetComponent<DestroyableController>().Damage(i);
        }
    }
    public static void Damage(this MonoBehaviour m, int i, string s)
    {
        /*
         Same as above, but takes DoTs into account
         */
        if (m is DestroyableController d)
        {
            d.Damage(i, s);
        }
        else if (m is EnemyController e)
        {
            Debug.Log("Status Attack");
            e.Damage(i, s);
        }
        else if (m is PlayerController p)
        {
            p.Damage(i, s);
        }
        else if (m is BulletController b)
        {
            b.gameObject.GetComponent<DestroyableController>().Damage(i, s);
        }
        else if (m is RangedAttack r)
        {
            r.gameObject.GetComponent<DestroyableController>().Damage(i, s);
        }
    }

    public static MonoBehaviour PrimaryController(this GameObject g) {
        switch (g.tag)
        {
            case "Player":
                return g.GetComponent<PlayerController>();
            case "GameController":
                return g.GetComponent<GameController>();
            case "Enemy":
                //If you DON'T want to grab a particular enemy controller and want to use generic as primary, please remove the below if-statements
                if (g.GetComponent<EnemyController>() is RangedEnemy r)
                {
                    return r;
                }
                else if (g.GetComponent<EnemyController>() is MeleeEnemy m)
                {
                    return m;
                }
                return g.GetComponent<EnemyController>();
            case "Item":
                return g.GetComponent<ItemController>();
            case "Destroyable":
                return g.GetComponent<DestroyableController>();
            case "EnemyRanged":
                return g.GetComponent<BulletController>();
            case "PlayerRanged":
                return g.GetComponent<RangedAttack>();
        }
        return null;
    }

    public static Collider2D PrimaryCollider(this GameObject g) {
        switch (g.tag)
        {
            case "Player":
                return g.GetComponent<PlayerController>().PlayerCollider;
            case "Enemy":
                return g.GetComponent<EnemyController>().HitBox;
            case "Item":
                return g.GetComponent<ItemController>().collider;
            case "Destroyable":
            case "Wall":
                return g.GetComponent<BoxCollider2D>();
            case "PlayerRanged":
                return g.GetComponent<RangedAttack>().ColliderGet;
            case "EnemyRanged":
                return g.GetComponent<Collider2D>();
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

    public static int[,] Expand(this int[,] o, int[] value, int curLength)
    {
        int[,] temp = o;
        o = new int[curLength + 1, value.Length];
        for (int z = 0; z < curLength; ++z)
        {
            for (int y = 0; y < value.Length; ++y)
            {
                o[z,y] = temp[z,y];
            }
        }
        for (int z = 0; z < value.Length; ++z)
        {
            o[curLength,z] = value[z];
        }
        return o;
    }
}
