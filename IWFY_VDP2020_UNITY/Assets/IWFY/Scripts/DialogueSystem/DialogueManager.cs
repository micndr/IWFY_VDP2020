using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    
    private Coroutine sentencesAnimation;
    
    private Queue<string> sentences; //Fifo collection

    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    // Jacopo -> added a reference to call triggerer from the starting object, 
    // if another method is found, please use it.
    private DialogueTrigger startPointRef;

    private InventoryManager inventoryState;
    //private GameObject inventoryState;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        inventoryState = FindObjectOfType<InventoryManager>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger startpoint)
    {
        startPointRef = startpoint;
        animator.SetBool(IsOpen, true);
        nameText.text = dialogue.name;
        sentences.Clear();
        //inventoryState.setInventoryState(false);
        inventoryState.setInventoryState(false);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (sentencesAnimation != null) StopCoroutine(sentencesAnimation); 
        sentencesAnimation = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.1f);
            //yield return null; //how much does wait
        }
    }

    void EndDialogue() 
    {
        animator.SetBool(IsOpen, false);
        inventoryState.setInventoryState(true);
        if (startPointRef) {
            Triggerer tr = startPointRef.GetComponent<Triggerer>(); 
            if (tr) tr.Trigger();
        }
    }
}
