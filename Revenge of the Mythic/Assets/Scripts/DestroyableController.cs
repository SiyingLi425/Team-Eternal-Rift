using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableController : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    public void Damage (int i)
    {
        if (i > 0)
        {
            Destroy(gameObject);
            gameController.breakSound.Play();
        }
    }
    public void Damage (int i, string s)
    {
        if (i > 0)
        {
            Destroy(gameObject);
            gameController.breakSound.Play();
        }
    }
}
