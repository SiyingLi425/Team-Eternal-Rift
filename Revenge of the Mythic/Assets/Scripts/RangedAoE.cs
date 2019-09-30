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
        //Do 2 for-loops to check the entire screen - NOTE: Requires Game Controller
        Vector3 temp = new Vector3();
        foreach (string s in targets)
        {

        }
        return temp;
    }
}
