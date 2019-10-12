using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAoE : RangedAttack //Because this inherits from RangedAttack, it's possible to create moving AoEs
{
    [SerializeField]
    private string dots = "";
    [SerializeField]
    private int globalCooldownReset;
    private int globalCooldown = 0;
    private bool testing = true;

    [SerializeField]
    private GameObject tester;
    // Start is called before the first frame update
    void Start()
    {
        Tick();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCopy();
        --globalCooldown;
        if (globalCooldown <= 0)
        {
            Tick();
        }
    }

    public Vector3 OptimalSpawnPoint(Collider2D col)
    {
        #region Initialize Variables
        //GameController c = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Vector2 point = new Vector2(0, 0);
        int[,] priorities = new int[0,3];
        int prioritiesSize = 0;
        float size = 0.00f;
        if (col is BoxCollider2D b)
        {
            size = b.size.x > b.size.y ? b.size.x/2 : b.size.y/2;
        }
        else if (col is CircleCollider2D c)
        {
            size = c.radius;
        }
        else if (col is CapsuleCollider2D a)
        {
            size = a.size.x > a.size.y ? a.size.x / 2 : a.size.y / 2;
        }
        #endregion
        #region Playable Area Loop
        for (float x = col.bounds.center.x - size; x < col.bounds.center.x + size; x += 0.1f)
        {
            point.x = x;
            for (float y = col.bounds.center.y - size; y < col.bounds.center.y + size; y += 0.1f)
            {
                point.y = y;
                if (col.bounds.Contains(point)) //If the current point is within the provided range
                {
                    #region Initialize Variables
                    int priority = 0;
                    GameObject temp = Instantiate(tester, point, Quaternion.identity);
                    {
                        CircleCollider2D testCol = temp.GetComponent<CircleCollider2D>();
                        testCol.gameObject.transform.position = point;
                        #endregion
                        #region Set priority
                        foreach (string s in targets)
                        {
                            foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
                            {
                                if (testCol.bounds.Contains(g.transform.position))
                                {
                                    ++priority;
                                }
                            }
                        }
                    }
                    Destroy(temp);
                    #endregion
                    if (priority > 0)
                    {
                        int[] val = { (int) point.x, (int) point.y, priority };
                        priorities = priorities.Expand(val, prioritiesSize);
                        ++prioritiesSize;
                    }
                }
            }
        }
        testing = false;
        #endregion
        #region Find Maximum Priority
        int max = 0, index = 0, total = 0; ;
        for (int z = 0; z < prioritiesSize; ++z)
        {
            if (priorities[z, 2] > max)
            {
                max = priorities[z, 2];
                index = z;
                total = 1;
            }
            else if (priorities[z, 2] == max)
            {
                ++total;
            }
        }
        #endregion
        #region Return
        if (total == 1)
        {
            return new Vector2(priorities[index, 0], priorities[index, 1]);
        }
        else if (total > 1)
        {
            Vector2[] prio = new Vector2[prioritiesSize];
            for (int z=0; z<prio.Length; ++z)
            {
                prio[z] = new Vector2(priorities[z, 0], priorities[z, 1]);
            }
            return GetClosestPoint(col.bounds.center, prio);
        }
        return col.bounds.center;
        #endregion
    }

    protected Vector2 GetClosestPoint(Vector2 startPoint, Vector2[] comparePoints)
    {
        float[] dif = new float[comparePoints.Length];
        float min = Mathf.Infinity;
        int index = 0;
        for (int z = 0; z < comparePoints.Length; ++z)
        {
            dif[z] = Mathf.Abs(startPoint.x - comparePoints[z].x) + Mathf.Abs(startPoint.y - comparePoints[z].y);
            if (dif[z] < min)
            {
                min = dif[z];
                index = z;
            }
        }
        return comparePoints[index];
    }

    private void Tick()
    {
        if (testing == false)
        {
            globalCooldown = globalCooldownReset;
            foreach (string s in targets)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
                {
                    if (dots == "")
                    {
                        g.PrimaryController().Damage(damage);
                    }
                    else
                    {
                        g.PrimaryController().Damage(damage, dots);
                    }
                }
            }
        }
    }
}
