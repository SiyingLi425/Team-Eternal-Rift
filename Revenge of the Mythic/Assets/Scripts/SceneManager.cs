using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public PersisableObjects persisableObject;
    public GameObject playerSelection;
    private GameObject charChoice, gameMode, startButton, backButton;
    private Text highscore;
    private string firstScene = "GameScene"; //modify this to start at a particular level

    private int totalPlayers = 0;
    private int playerNum = 0;
    private int wait = 1;

    void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StartScene")
        {
            charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
            gameMode = GameObject.FindGameObjectWithTag("GameMode");
            playerSelection = GameObject.FindGameObjectWithTag("PlayerSelection");
            startButton = GameObject.FindGameObjectWithTag("StartButton");
            backButton = GameObject.FindGameObjectWithTag("BackButton");
            backButton.SetActive(false);
            if (PersisableObjects.isCreated == true)
            {
                persisableObject = GameObject.FindGameObjectWithTag("PersisableObject").GetComponent<PersisableObjects>();
            }
        }
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Victory" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameOver")
        {
            highscore = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
            persisableObject = GameObject.FindGameObjectWithTag("PersisableObject").GetComponent<PersisableObjects>();
            highscore.text = "Highscore: "+ persisableObject.score;
        }
        

    }


    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Victory" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "GameOver")
        {
            if (playerNum == 1)
            {
                playerSelection.GetComponent<Text>().text = "Player " + (playerNum + 1);
            }
            if (wait > 0)
            {
                wait--;
            }
            else if (wait == 0)
            {
                gameMode.SetActive(false);
                playerSelection.SetActive(false);
                charChoice.GetComponent<CanvasGroup>().alpha = 0f;
                charChoice.GetComponent<CanvasGroup>().interactable = false;
                charChoice.GetComponent<CanvasGroup>().blocksRaycasts = false;
                //charChoice.SetActive(false);
                wait = -1;
            }
        }
        
    }
    public void onClickStart()
    {
        gameMode.SetActive(true);
        startButton.SetActive(false);
        persisableObject.playerTypes.Clear();
        playerNum = 0;
        playerSelection.GetComponent<Text>().text = "Player 1";
        GoalController.level = 1;
    }

    public void onClickRestart()
    {
        persisableObject.clear();
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
    public void onClickBack()
    {
        gameMode.SetActive(true);
        //charChoice.SetActive(false);
        charChoice.GetComponent<CanvasGroup>().alpha = 0f;
        charChoice.GetComponent<CanvasGroup>().interactable = false;
        charChoice.GetComponent<CanvasGroup>().blocksRaycasts = false;
        backButton.SetActive(false);
        playerSelection.SetActive(false);
        persisableObject.playerTypes.Clear();
        playerNum = 0;
        playerSelection.GetComponent<Text>().text = "Player 1";
    }
    public void onClickSinglePlayer()
    {
        gameMode.SetActive(false);
        //charChoice.SetActive(true);
        charChoice.GetComponent<CanvasGroup>().alpha = 1f;
        charChoice.GetComponent<CanvasGroup>().interactable = true;
        charChoice.GetComponent<CanvasGroup>().blocksRaycasts = true;
        totalPlayers = 1;
        persisableObject.totalPlayers = 1;
        backButton.SetActive(true);
    }
    public void onClickMultiPlayer()
    {
        gameMode.SetActive(false);
        //charChoice.SetActive(true);
        charChoice.GetComponent<CanvasGroup>().alpha = 1f;
        charChoice.GetComponent<CanvasGroup>().interactable = true;
        charChoice.GetComponent<CanvasGroup>().blocksRaycasts = true;
        playerSelection.SetActive(true);
        totalPlayers = 2;
        persisableObject.totalPlayers = 2;
        backButton.SetActive(true);
    }
    public void onClickPhonix()
    {
        persisableObject.playerTypes.Add(1);
        playerNum++;
        //GameObject.FindGameObjectWithTag("PlayPhoenix").GetComponent<PhoenixMouseOver>().closeDesc();
        //GameObject.FindGameObjectWithTag("PlayPhoenix").SetActive(false);
        if(playerNum == totalPlayers)
        {
            LoadScene();
        }
        
            
    }
    public void onClickGriffin()
    {
        persisableObject.playerTypes.Add(2);
        playerNum++;
        GameObject.FindGameObjectWithTag("PlayGriffin").GetComponent<GriffinMouseOver>().closeDesc();
        GameObject.FindGameObjectWithTag("PlayGriffin").SetActive(false);

        if (playerNum == totalPlayers)
        {
            LoadScene();
        }

    }
    public void onClickDragon()
    {
        persisableObject.playerTypes.Add(3);
        playerNum++;
        GameObject.FindGameObjectWithTag("PlayDragon").GetComponent<DragonMouseOver>().closeDesc();
        GameObject.FindGameObjectWithTag("PlayDragon").SetActive(false);
        if (playerNum == totalPlayers)
        {
            LoadScene();
        }
    }
    public void onClickPegasus()
    {
        persisableObject.playerTypes.Add(4);
        playerNum++;
        GameObject.FindGameObjectWithTag("PlayPegasus").GetComponent<PegasusMouseOver>().closeDesc();
        GameObject.FindGameObjectWithTag("PlayPegasus").SetActive(false);
        if (playerNum == totalPlayers)
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
        if(PersisableObjects.isCreated == false)
        {
            DontDestroyOnLoad(persisableObject);
            PersisableObjects.isCreated = true;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(firstScene);
    }




}
