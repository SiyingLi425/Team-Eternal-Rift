using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public PersisableObjects persisableObject;
    public GameObject playerSelection;
    private GameObject charChoice, gameMode;
    private string firstScene = "GameScene"; //modify this to start at a particular level

    private int totalPlayers = 0;
    private int playerNum = 0;

    void Start()
    {
        charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        playerSelection = GameObject.FindGameObjectWithTag("PlayerSelection");
        charChoice.SetActive(false);
        gameMode.SetActive(false);
        playerSelection.SetActive(false);
    }

    private void Update()
    {
        if(playerNum == 1)
        {
            playerSelection.GetComponent<Text>().text = "Player " + (playerNum + 1);
        }
        
    }
    public void onClickStart()
    {
        gameMode.SetActive(true);
    }

    public void onClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
    public void onClickSinglePlayer()
    {
        gameMode.SetActive(false);
        charChoice.SetActive(true);
        totalPlayers = 1;
        persisableObject.totalPlayers = 1;
    }
    public void onClickMultiPlayer()
    {
        gameMode.SetActive(false);
        charChoice.SetActive(true);
        playerSelection.SetActive(true);
        totalPlayers = 2;
        persisableObject.totalPlayers = 2;
    }
    public void onClickPhonix()
    {

        persisableObject.playerTypes.Add(1);
        playerNum++;
        if(playerNum == totalPlayers)
        {
            LoadScene();
        }
        
            
    }
    public void onClickGriffin()
    {
        persisableObject.playerTypes.Add(2);
        playerNum++;
        if (playerNum == totalPlayers)
        {
            LoadScene();
        }

    }
    public void onClickDragon()
    {
        persisableObject.playerTypes.Add(3);
        playerNum++;
        if (playerNum == totalPlayers)
        {
            LoadScene();
        }
    }
    public void onClickPegasus()
    {
        persisableObject.playerTypes.Add(4);
        playerNum++;
        if (playerNum == totalPlayers)
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
        DontDestroyOnLoad(persisableObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(firstScene);
    }




}
