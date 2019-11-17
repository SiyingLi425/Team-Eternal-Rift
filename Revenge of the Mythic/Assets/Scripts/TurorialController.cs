using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurorialController : MonoBehaviour
{
    public Text dialogueTextHolder;
    public GameObject npcBox;
    public Text nameHolder;

    //[TextArea(3, 10)]
    //public string[] sentences;
    public string speakerName;

    private GameObject player;
    private Collider2D interactArea, playerCollider;

    //public Queue<string> sentencesQueve;
    //private string sentence;
    private bool started = false;

    public TextAsset interactText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactArea = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();
        npcBox = GameObject.FindGameObjectWithTag("NPC");
        nameHolder = GameObject.FindGameObjectWithTag("SpeakerName").GetComponent<Text>();
        dialogueTextHolder =GameObject.FindGameObjectWithTag("DialogueTextHolder").GetComponent<Text>();

        if (interactArea.IsTouching(playerCollider))
        {
            StartDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollider.IsTouching(interactArea) && started == false)
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EndDialogue();
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag == "Player" && started == false)
    //    {
    //        StartDialogue();
    //    }  
    //}

    public void StartDialogue()
    {
        started = true;
        npcBox.SetActive(true);
        nameHolder.text = speakerName;
        dialogueTextHolder.text = interactText.text;
    }


    void EndDialogue()
    {
        dialogueTextHolder.text = "";
        npcBox.SetActive(false);
        started = false;
    }
}
