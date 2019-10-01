using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAoE : RangedAttack //Because this inherits from RangedAttack, it's possible to create moving AoEs
{
    [SerializeField]
    private int[] attackPriority;
    [SerializeField]
    private Collider2D size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 OptimalSpawnPoint(Collider2D col)
    {
        #region Initialize Variables
        GameController c = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Vector2 point = new Vector2(0, 0);
        int[,] priorities = new int[0,3];
        int prioritiesSize = 0;
        #endregion
        #region Playable Area Loop
        for (int x=0; x<c.GameWidth; ++x)
        {
            point.x = x;
            for (int y=0; y>c.GameHeight; ++y)
            {
                point.y = y;
                if (col.bounds.Contains(point)) //If the current point is within the provided range
                {
                    #region Initialize Variables
                    Collider2D testCol = size;
                    testCol.transform.position = point;
                    int priority = 0;
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
                    #endregion
                    if (priority > 0)
                    {
                        int[] val = { (int) point.x, (int) point.y, priority };
                        priorities = priorities.Expand(val, 3);
                        ++prioritiesSize;
                    }
                }
            }
        }
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
}
