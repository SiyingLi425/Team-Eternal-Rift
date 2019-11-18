using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Public variables
    public TextAsset map;
    public GameObject wall, floor, destroyableObj, undestroyableObj, meleeEnemy, rangedEnemy, gas, bonusRabbit, door, griffon, phoenix, tutorialBird, gold, silver, boss, dragon, pegasus, chimera, scientist;
    public float gridSize = 1;
    public Text playerHealth, ability1CD, ability2CD, ability3CD;
    public PlayerController playerController;
    public TextAsset[] tutorialText = new TextAsset[9];


    [Header("ScoreBoard")]
    public int score;
    public GameObject scoreText;

    [Header("Audio Controller")]
    public AudioSource breakSound;

    private int playerNum = 0;
    private int health;
    private PersisableObjects persisableObjects;
    private List<int> playerTypes;
    public int PlayerNum { get { return playerNum; } }
    // Start is called before the first frame update
    void Start()
    {
        persisableObjects = GameObject.FindGameObjectWithTag("PersisableObject").GetComponent<PersisableObjects>();
        playerTypes = persisableObjects.playerTypes;
        LoadRoom(map);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerHealth = GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>();
        ability1CD = GameObject.FindGameObjectWithTag("P1Ability1CD").GetComponent<Text>();
        ability2CD = GameObject.FindGameObjectWithTag("P1Ability2CD").GetComponent<Text>();
        ability3CD = GameObject.FindGameObjectWithTag("P1Ability3CD").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText");


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
                        Instantiate(tutorialBird, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "O":
                        Instantiate(gold, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "Q":
                        Instantiate(silver, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "S":
                        Instantiate(boss, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "C":
                        Instantiate(chimera, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                    case "I":
                        Instantiate(scientist, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                }
            }
        }
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("Tutorial");
        for (int z = 0, o = 0, r = 0, w = 0; z < NPCs.Length; ++z)
        {
            if (NPCs[z].name.Contains("Tutorial"))
            {
                NPCs[z].GetComponent<TurorialController>().interactText = tutorialText[tutorialText.Length - 1 - o];
                ++o;
            }
           
        }
    }
    public void AddPlayer(float x, float y)
    {
        for(int i = 0; i < persisableObjects.totalPlayers; i++)
        {
        
        GameObject player = new GameObject();
        if (playerTypes[i] == 1)
        {
            player = phoenix;
        }else if(playerTypes[i] == 2)
        {
            player = griffon;
        }
        else if (playerTypes[i] == 3)
        {
            player = dragon;
        }
        else if (playerTypes[i] == 4)
        {
            player = pegasus;
        }

        Instantiate(player, new Vector2(x, y), transform.rotation);
            playerNum++;
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }
}
