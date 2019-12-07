using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Public variables
    public TextAsset map;
    public GameObject wall, floor, destroyableObj, undestroyableObj, meleeEnemy, rangedEnemy, gas, lifeGem, door, griffon, phoenix, tutorialBird, gold, silver, boss, dragon, pegasus, chimera, scientist, light;
    public float gridSize = 1;
    public Text playerHealth1, playerHealth2;
    public TextMeshProUGUI ability1CD1, ability2CD1, ability3CD1, ability1CD2, ability2CD2, ability3CD2;
    public Image Ability1Icon, Ability2Icon, Ability3Icon;
    public PlayerController playerController1, playerController2;
    public TextAsset[] tutorialText = new TextAsset[9];


    [Header("ScoreBoard")]
    public int score;
    public GameObject scoreText;

    [Header("Audio Controller")]
    public AudioSource breakSound;
    public AudioSource coinSound;

    private int playerNum = 0;
    private int health;
    private PersisableObjects persisableObjects;
    private List<int> playerTypes;
    private GameObject player2UI;
    private int bossCount = 0;
    private GameObject npcDialogue;

    [Header("UI Controller")]
    public List<Sprite> pheonixAbilityIcons;
    public List<Sprite> griffinAbilityIcons;
    public List<Sprite> dragonAbilityIcons;
    public List<Sprite> pegasusAbilityIcons;


    //hp flashing variables
    private int p1Hp = -1, flashTimer, flashTime = 10;
    private int p2Hp = -1, flashTimer2;
    public int PlayerNum { get { return playerNum; } }

    //ability cooldown reset keys
    private string[,] resetKeys = { {"Z", "X", "C" }, {",", ".", "/" } };
    public string[,] ResetKeys { get { return resetKeys; } }

    // Start is called before the first frame update
    void Start()
    {
        npcDialogue = GameObject.FindGameObjectWithTag("NPC");
        persisableObjects = GameObject.FindGameObjectWithTag("PersisableObject").GetComponent<PersisableObjects>();
        playerTypes = persisableObjects.playerTypes;
        LoadRoom(map);
        #region UI Setting
        player2UI = GameObject.FindGameObjectWithTag("Player2UI");
        player2UI.SetActive(false);
        playerController1 = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        playerHealth1 = GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>();
        Ability1Icon = GameObject.FindGameObjectWithTag("P1Ability1Icon").GetComponent<Image>();
        Ability2Icon = GameObject.FindGameObjectWithTag("P1Ability2Icon").GetComponent<Image>();
        Ability3Icon = GameObject.FindGameObjectWithTag("P1Ability3Icon").GetComponent<Image>();
        ability1CD1 = GameObject.FindGameObjectWithTag("P1Ability1CD").GetComponent<TextMeshProUGUI>();
        ability2CD1 = GameObject.FindGameObjectWithTag("P1Ability2CD").GetComponent<TextMeshProUGUI>();
        ability3CD1 = GameObject.FindGameObjectWithTag("P1Ability3CD").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        playerController1.Health = persisableObjects.player1hp;
        switch (playerTypes[0])
        {
            case 1:
                Ability1Icon.sprite = pheonixAbilityIcons[0];
                Ability2Icon.sprite = pheonixAbilityIcons[1];
                Ability3Icon.sprite = pheonixAbilityIcons[2];
                break;
            case 2:
                Ability1Icon.sprite = griffinAbilityIcons[0];
                Ability2Icon.sprite = griffinAbilityIcons[1];
                Ability3Icon.sprite = griffinAbilityIcons[2];
                break;
            case 3:
                Ability1Icon.sprite = dragonAbilityIcons[0];
                Ability2Icon.sprite = dragonAbilityIcons[1];
                Ability3Icon.sprite = dragonAbilityIcons[2];
                break;
            case 4:
                Ability1Icon.sprite = pegasusAbilityIcons[0];
                Ability2Icon.sprite = pegasusAbilityIcons[1];
                Ability3Icon.sprite = pegasusAbilityIcons[2];
                break;
        }

        if (persisableObjects.totalPlayers == 2)
        {
            player2UI.SetActive(true);
            playerController2 = GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerController>();
            playerHealth2 = GameObject.FindGameObjectWithTag("Player2Health").GetComponent<Text>();
            Ability1Icon = GameObject.FindGameObjectWithTag("P2Ability1Icon").GetComponent<Image>();
            Ability2Icon = GameObject.FindGameObjectWithTag("P2Ability2Icon").GetComponent<Image>();
            Ability3Icon = GameObject.FindGameObjectWithTag("P2Ability3Icon").GetComponent<Image>();
            ability1CD2 = GameObject.FindGameObjectWithTag("P2Ability1CD").GetComponent<TextMeshProUGUI>();
            ability2CD2 = GameObject.FindGameObjectWithTag("P2Ability2CD").GetComponent<TextMeshProUGUI>();
            ability3CD2 = GameObject.FindGameObjectWithTag("P2Ability3CD").GetComponent<TextMeshProUGUI>();
            playerController2.Health = persisableObjects.player2hp;

            switch (playerTypes[1])
            {
                case 1:
                    Ability1Icon.sprite = pheonixAbilityIcons[0];
                    Ability2Icon.sprite = pheonixAbilityIcons[1];
                    Ability3Icon.sprite = pheonixAbilityIcons[2];
                    break;
                case 2:
                    Ability1Icon.sprite = griffinAbilityIcons[0];
                    Ability2Icon.sprite = griffinAbilityIcons[1];
                    Ability3Icon.sprite = griffinAbilityIcons[2];
                    break;
                case 3:
                    Ability1Icon.sprite = dragonAbilityIcons[0];
                    Ability2Icon.sprite = dragonAbilityIcons[1];
                    Ability3Icon.sprite = dragonAbilityIcons[2];
                    break;
                case 4:
                    Ability1Icon.sprite = pegasusAbilityIcons[0];
                    Ability2Icon.sprite = pegasusAbilityIcons[1];
                    Ability3Icon.sprite = pegasusAbilityIcons[2];
                    break;
            }
        }
        score = persisableObjects.score;
        scoreText.GetComponent<Text>().text = "Score: " + persisableObjects.score;
        if (GoalController.level != 1)
        {
            npcDialogue.SetActive(false); 
        }

        #endregion
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Player1 HP Flashing
        if (p1Hp == -1)
        {
            p1Hp = playerController1.Health;
        }

        if (p1Hp != playerController1.Health)
        {

            flashTimer = flashTime;

        }
        if (flashTimer > 0)
        {
            flashTimer--;
            if (p1Hp > playerController1.Health)
            {
                GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>().color = Color.red;
            }
            if (p1Hp < playerController1.Health)
            {
                GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>().color = Color.green;
            }
            p1Hp = playerController1.Health;

        }

        if (flashTimer == 0)
        {
            GameObject.FindGameObjectWithTag("Player1Health").GetComponent<Text>().color = Color.black;
        }
        #endregion
        #region Player2 HP Flashing
        if (persisableObjects.totalPlayers == 2)
        {
            if (p2Hp == -1)
            {
                p2Hp = playerController2.Health;
            }

            if (p2Hp != playerController2.Health)
            {

                flashTimer2 = flashTime;

            }
            if (flashTimer2 > 0)
            {
                flashTimer2--;
                if (p2Hp > playerController2.Health)
                {
                    GameObject.FindGameObjectWithTag("Player2Health").GetComponent<Text>().color = Color.red;
                }
                if (p2Hp < playerController2.Health)
                {
                    GameObject.FindGameObjectWithTag("Player2Health").GetComponent<Text>().color = Color.green;
                }
                p2Hp = playerController2.Health;

            }

            if (flashTimer2 == 0)
            {
                GameObject.FindGameObjectWithTag("Player2Health").GetComponent<Text>().color = Color.black;
            }
        }
        #endregion


        playerHealth1.text = "Health: " + playerController1.Health;
        ability1CD1.text = playerController1.AbilityCoolDown[0] == 0 ? resetKeys[0, 0] : $"{playerController1.AbilityCoolDown[0] / 50}";
        ability2CD1.text = playerController1.AbilityCoolDown[1] == 0 ? resetKeys[0, 1] : $"{playerController1.AbilityCoolDown[1] / 50}";
        ability3CD1.text = playerController1.AbilityCoolDown[2] == 0 ? resetKeys[0, 2] : $"{playerController1.AbilityCoolDown[2] / 50}";

        if (playerController1.AbilityCoolDown[0] != 0)
        {
            GameObject.FindGameObjectWithTag("P1Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GameObject.FindGameObjectWithTag("P1Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (playerController1.AbilityCoolDown[1] != 0)
        {
            GameObject.FindGameObjectWithTag("P1Ability2CD").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GameObject.FindGameObjectWithTag("P1Ability2CD").GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (playerController1.AbilityCoolDown[2] != 0)
        {
            GameObject.FindGameObjectWithTag("P1Ability3CD").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GameObject.FindGameObjectWithTag("P1Ability3CD").GetComponent<TextMeshProUGUI>().color = Color.green;
        }



        if (persisableObjects.totalPlayers == 2)
        {
            playerHealth2.text = "Health: " + playerController2.Health;
            ability1CD2.text = playerController2.AbilityCoolDown[0] == 0 ? resetKeys[1, 0] : $"{playerController2.AbilityCoolDown[0] / 50}";
            ability2CD2.text = playerController2.AbilityCoolDown[1] == 0 ? resetKeys[1, 1] : $"{playerController2.AbilityCoolDown[1] / 50}";
            ability3CD2.text = playerController2.AbilityCoolDown[2] == 0 ? resetKeys[1, 2] : $"{playerController2.AbilityCoolDown[2] / 50}";
        }
        if (persisableObjects.totalPlayers == 2)
        {
            if (playerController2.AbilityCoolDown[0] != 0)
            {
                GameObject.FindGameObjectWithTag("P2Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                GameObject.FindGameObjectWithTag("P2Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            if (playerController2.AbilityCoolDown[1] != 0)
            {
                GameObject.FindGameObjectWithTag("P2Ability2CD").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                GameObject.FindGameObjectWithTag("P2Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            if (playerController2.AbilityCoolDown[2] != 0)
            {
                GameObject.FindGameObjectWithTag("P2Ability3CD").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            else
            {
                GameObject.FindGameObjectWithTag("P2Ability1CD").GetComponent<TextMeshProUGUI>().color = Color.green;
            }
        }
    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
    public void Victory()
    {
        bossCount++;
        if(bossCount == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }
        
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
                    case "A":
                        if (persisableObjects.totalPlayers == 2)
                        {
                            AddPlayer(xAxis, yAxis);
                        }
                        else
                        {
                            Instantiate(floor, new Vector2(xAxis, yAxis), transform.rotation);
                        }
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
                        Instantiate(lifeGem, new Vector2(xAxis, yAxis), transform.rotation);
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
                    case "Y":
                        Instantiate(light, new Vector2(xAxis, yAxis), transform.rotation);
                        break;
                }
            }
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Light"))
        {
            g.GetComponent<LightController>().Start();
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
    public void ChangeResetKey (int player, int key, string value)
    {
        resetKeys[player, key] = value;
    }
    public void AddPlayer(float x, float y)
    {
        int i = GameObject.FindGameObjectsWithTag("Player").Length;
        GameObject player = new GameObject();
        if (playerTypes[i] == 1)
        {
            player = phoenix;
        }
        else if(playerTypes[i] == 2)
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

    }

    public void IndexPlayer()
    {
        ++playerNum;
    }
    public void AddScore(int amount)
    {
        score += amount;
        persisableObjects.score = score;
        coinSound.Play();
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }
    //public void updatePlayerHP()
    //{
        
    //}

}
