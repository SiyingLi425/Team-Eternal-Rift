using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public variables
    public TextAsset map;
    public GameObject wall, floor, destroyableObj, undestroyableObj, meleeEnemy, rangedEnemy, gas, bonusRabbit, door, griffon, phoenix;
    public float gridSize = 1;

    private int gameWidth, gameHeight; //Size of the playable area on the screen, in pixels. If this is no longer the case, please fix OptimalSpawnPoint in RangedAoE
    private int playerNum = 0;
    public int GameWidth { get { return gameWidth; } }
    public int GameHeight { get { return gameHeight; } }
    public int PlayerNum { get { return playerNum; } }
    // Start is called before the first frame update
    void Start()
    {
        LoadRoom(map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() { }

    //This function makes the map according to the textfile that has been assigned
    public void LoadRoom(TextAsset map)
    {
        string[] temp = map.text.Split('\n');
        string[] tempReverse = new string[temp.Length];
        for (int z = 0; z < temp.Length; ++z)
        {
            tempReverse[z] = temp[z];
        }
        temp = tempReverse;
        for (int z = 0; z < temp.Length; ++z)
        {
            string[] doubleTemp = temp[z].Split('-');
            for (int y = 0; y < doubleTemp.Length; ++y)
            {
                float xAxis = y * gridSize, yAxis = (temp.Length - 1 - z) * gridSize;
                switch (doubleTemp[y].ToUpper())
                {
                    case "W":
                        break;
                    default:
                        Instantiate(floor, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                }
                switch (doubleTemp[y].ToUpper())
                {
                    case "W":
                        Instantiate(wall, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "P":
                        AddPlayer(xAxis, yAxis);
                        break;
                    case "B":
                        Instantiate(destroyableObj, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "U":
                        Instantiate(undestroyableObj, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "M":
                        Instantiate(meleeEnemy, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "R":
                        Instantiate(rangedEnemy, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "G":
                        Instantiate(gas, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "H":
                        Instantiate(bonusRabbit, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "D":
                        Instantiate(door, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                }
            }
        }
    }
    public void AddPlayer(float x, float y)
    {
        ++playerNum;
        GameObject player = playerNum == 1 ? griffon : phoenix;
        Instantiate(player, new Vector2(x, y), transform.rotation);
    }
}
