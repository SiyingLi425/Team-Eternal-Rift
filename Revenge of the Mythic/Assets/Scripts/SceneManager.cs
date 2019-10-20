using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PersisableObjects persisableObject;
    private GameObject charChoice;

    void Start()
    {
        charChoice = GameObject.FindGameObjectWithTag("CharacterChoice");
        charChoice.SetActive(false);
    }
    public void onClickStart()
    {
        
        charChoice.SetActive(true);
        
    }

    public void onClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void onClickPhonix()
    {
        persisableObject.playerType = 1;
        DontDestroyOnLoad(persisableObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void onClickGriffin()
    {

        persisableObject.playerType = 2;
        DontDestroyOnLoad(persisableObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    

    
}
