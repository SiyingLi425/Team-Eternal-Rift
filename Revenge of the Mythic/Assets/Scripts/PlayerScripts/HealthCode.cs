using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCode : MonoBehaviour
{
    private KeyCode[] healthCode =
    {
        KeyCode.H,
        KeyCode.E,
        KeyCode.A,
        KeyCode.L,
        KeyCode.T,
        KeyCode.H
    };
    private int health = 0;
    public int Health { get { return health; } }
    public int HealthCodeLength { get { return healthCode.Length; } }

    void Update()
    {
        if (health < healthCode.Length)
        {
            if (Input.GetKeyDown(healthCode[health]))
            {
                ++health;
                if (health == healthCode.Length)
                {
                    try
                    {
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            g.GetComponent<PlayerController>().HealthCode = true;
                        }
                    }
                    catch (System.NullReferenceException) { }
                }
            }
            else if (Input.anyKeyDown)
            {
                health = 0;
            }
        }
    }
}
