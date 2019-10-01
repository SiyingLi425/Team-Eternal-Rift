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
        GameController c = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Vector2 point = new Vector3(0,0);
        for (int x=0; x<c.GameWidth; ++x)
        {
            point.x = x;
            for (int y=0; y>c.GameHeight; ++y)
            {
                point.y = y;
                if (col.bounds.Contains(point)) {
                    //Create a Collider2D on that point
                    Collider2D test = new Collider2D();

                    int priority = 0;
                    foreach (string s in targets)
                    {
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag(s))
                        {
                            if (col.bounds.Contains(g.transform.position))
                            {
                                ++priority;
                            }
                        }
                    }
                }
            }
        }
        Vector3 temp = new Vector3();
        foreach (string s in targets)
        {

        }
        return temp;
    }
}
