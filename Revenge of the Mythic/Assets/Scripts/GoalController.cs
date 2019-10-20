using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private Collider2D goalCollider, playerCollider;
    public static int level;
    
    // Start is called before the first frame update
    void Start()
    {
        goalCollider = this.GetComponent<BoxCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollider.IsTouching(goalCollider))
        {
            if(level < 3)
            {
                level++;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + (level + 1));
            }else if(level >= 3)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
            }

        }
    }
}
