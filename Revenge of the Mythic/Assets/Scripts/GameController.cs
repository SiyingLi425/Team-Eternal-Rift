using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Public variables
    public TextAsset map;
    public GameObject wall, floor, destroyableObj, undestroyableObj, meleeEnemy, rangedEnemy, gas, bonusRabbit, door, griffon, phoenix, tutorialBird;
    public float gridSize = 1;
    public Text playerHealth, ability1CD, ability2CD, ability3CD;
    public PlayerController playerController;

    private int playerNum = 0;
    private int health;
    public int PlayerNum { get { return playerNum; } }
    // Start is called before the first frame update
    void Start()
    {
        LoadRoom(map);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerHealth = GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>();
        ability1CD = GameObject.FindGameObjectWithTag("P1Ability1CD").GetComponent<Text>();
        ability2CD = GameObject.FindGameObjectWithTag("P1Ability2CD").GetComponent<Text>();
        ability3CD = GameObject.FindGameObjectWithTag("P1Ability3CD").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = "Health: " + playerController.Health;
        ability1CD.text = playerController.AbilityCoolDown[0] == 0 ? "R" : $"{playerController.AbilityCoolDown[0]/50}";
        ability2CD.text = playerController.AbilityCoolDown[1] == 0 ? "R" : $"{playerController.AbilityCoolDown[1] / 50}";
        ability3CD.text = playerController.AbilityCoolDown[2] == 0 ? "R" : $"{playerController.AbilityCoolDown[2] / 50}";
    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

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
                    case "T":
                        Instantiate(door, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "O":
                        Instantiate(tutorialBird, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                }
            }
        }
    }
    public void AddPlayer(float x, float y)
    {
        ++playerNum;
        GameObject player = playerNum == 1 ? phoenix  : griffon;
        Instantiate(player, new Vector2(x, y), transform.rotation);
    }
}
