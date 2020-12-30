using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public GameObject dialogue;
    public TextMeshProUGUI dialogueTMP;
    public TextMeshProUGUI nameTMP;
    
    public Animator animator;
    
    private Coroutine _sentencesAnimation;
    
    private Queue<string> _names; //Names unused in the new version
    private Queue<string> _sentences; //Fifo collection

    [SerializeField]
    public float _typespeed = 0.05f;
    [SerializeField]
    public float _autoadvancespeed = 0.5f;
    
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    // Jacopo -> added a reference to call trigger from the starting object, 
    // if another method is found, please use it.
    private DialogueTrigger _startPointRef;

    private InventoryManager _inventory;
    //private GameObject inventoryState;

    private bool _isTyping = false;
    public bool isInConversation = false;
    
    private string _sentence;
    // Start is called before the first frame update
    private void Awake()
    {
        _sentence = null;
        _names = new Queue<string>();
        _sentences = new Queue<string>();
        _inventory = FindObjectOfType<InventoryManager>();
        dialogueTMP = dialogue.GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger startpoint)
    {
        
        
        isInConversation = true;
        nameTMP.text = "Eia";
        _sentence = null;
        _isTyping = false; //false at start!
        _startPointRef = startpoint;
        animator.SetBool(IsOpen, true);
        _names.Clear();
        _sentences.Clear();
        
        _inventory.SetInventoryState(false);
        _inventory._inventoryDisplay.SetActive(false);
        _inventory._inventoryOff = true;
        foreach (string characterName in dialogue.names)
        {
            _names.Enqueue(characterName);
        }
        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
        
    }

    public void DisplayNextSentence()
    {
        
        if (_isTyping) //If yes complete the typing, at start is set to false to let the talking start
        {
            _isTyping = false;
            StopAllCoroutines();
            dialogueTMP.text = _sentence;
            StartCoroutine(AutoAdvance());
            //Debug.Log("I'm waiting");
            return;
        }
        
        if (_sentences.Count == 0 && _names.Count == 0) //Dialogue Ends here, when all queue are empty
        {
            _sentence = null;
            _names.Clear();
            _sentences.Clear();
            EndDialogue();
            return;
        }


        //throw exception when the name or sentence is empty
        if (_names.Count != 0)
        {
            nameTMP.text = _names.Dequeue();
            //nameText.text = _names.Dequeue();;
        }
        
        _sentence = _sentences.Dequeue(); 
        
        
        //if (_sentencesAnimation != null) StopCoroutine(_sentencesAnimation); 
        StopAllCoroutines(); //To avoid on creating another instance of AutoAdvance
        _sentencesAnimation = StartCoroutine(TypeSentence(_sentence));
        _isTyping = true; //I started a new sentence

    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueTMP.text = "";
        
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueTMP.text += letter;
            yield return new WaitForSeconds(_typespeed);
        }

        _isTyping = false;
        StartCoroutine(AutoAdvance());
    }

    private IEnumerator AutoAdvance()
    {
        yield return new WaitForSeconds(_autoadvancespeed);
        DisplayNextSentence();
    }

   

    private void EndDialogue()
    {
        isInConversation = false;
        animator.SetBool(IsOpen, false);
        _inventory.SetInventoryState(true);
        //Care here, destroy the trigger object after use
        if (!_startPointRef) return;
        Triggerer tr = _startPointRef.GetComponent<Triggerer>(); 
        if (tr) tr.Trigger();
    }
}
