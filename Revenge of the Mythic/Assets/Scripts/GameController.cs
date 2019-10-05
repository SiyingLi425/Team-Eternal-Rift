using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int gameWidth, gameHeight; //Size of the playable area on the screen, in pixels. If this is no longer the case, please fix OptimalSpawnPoint in RangedAoE
    private int playerNum = 1; //CHANGE TO 0 for playable
    public int GameWidth { get { return gameWidth; } }
    public int GameHeight { get { return gameHeight; } }
    public int PlayerNum { get { return playerNum; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() { }
    public void AddPlayer()
    {
        //instantiate player and up playerNum
    }
}
