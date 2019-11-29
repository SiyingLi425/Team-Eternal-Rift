using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private Collider2D goalCollider, playerCollider;
    public static int level = 1;
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        goalCollider = this.GetComponent<BoxCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //if (playerCollider.IsTouching(goalCollider) && timer == 0)
        //{
        //    Debug.Log("Level: " + level);
        //    if(level < 3)
        //    {
        //        level++;
        //        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + (level + 1));
        //    }else if(level >= 3)
        //    {
        //        UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        //    }
        //    timer = 200;

        //}
        //timer--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" &&(playerCollider.IsTouching(goalCollider)))
        {
            if (level < 3)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + (level + 1));
                level++;
            }
            else if (level >= 4)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
            }
        }
    }


}
