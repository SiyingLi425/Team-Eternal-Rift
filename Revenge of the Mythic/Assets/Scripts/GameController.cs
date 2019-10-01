using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int gameWidth, gameHeight; //Size of the playable area on the screen, in pixels. If this is no longer the case, please fix OptimalSpawnPoint in RangedAoE
    public int GameWidth { get; }
    public int GameHeight { get; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() { }
}
