using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurorialController : MonoBehaviour
{
    public Text dialogueTextHolder;
    public GameObject npcBox;
    public Text nameHolder;

    [TextArea(3, 10)]
    public string[] sentences;
    public string speakerName;

    private GameObject player;
    private Collider2D interactArea, playerCollider;

    public Queue<string> sentencesQueve;
   

    // Start is called before the first frame update
    void Start()
    {
        sentencesQueve = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
        interactArea = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollider.IsTouching(interactArea)){
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EndDialogue();
    }

    void interact()
    {

        StartDialogue();
        Debug.Log("Interact");
        
    }
    public void StartDialogue()
    {
        Debug.Log("Start");
        npcBox.SetActive(true);
        nameHolder.text = speakerName;
        sentencesQueve.Clear();
        foreach(string sentence in sentences)
        {
            sentencesQueve.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void  DisplayNextSentence()
    {
        if(sentencesQueve.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentencesQueve.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextHolder.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextHolder.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        npcBox.SetActive(false);
    }
}
