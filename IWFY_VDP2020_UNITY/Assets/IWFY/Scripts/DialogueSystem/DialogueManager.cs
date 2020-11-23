using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public GameObject dialogue;
    public TextMeshProUGUI dialogueTMP;
    
    public Animator animator;
    
    private Coroutine _sentencesAnimation;
    
    private Queue<string> _names;
    private Queue<string> _sentences; //Fifo collection

    [SerializeField]
    public float _typespeed = 0.05f;
    [SerializeField]
    public float _autoadvancespeed = 0.5f;
    
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    // Jacopo -> added a reference to call trigger from the starting object, 
    // if another method is found, please use it.
    private DialogueTrigger _startPointRef;

    private InventoryManager _inventoryState;
    //private GameObject inventoryState;
    // Start is called before the first frame update
    private void Awake()
    {
        _names = new Queue<string>();
        _sentences = new Queue<string>();
        _inventoryState = FindObjectOfType<InventoryManager>();
        dialogueTMP = dialogue.GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger startpoint)
    {
        _startPointRef = startpoint;
        animator.SetBool(IsOpen, true);
        _names.Clear();
        _sentences.Clear();
        
        _inventoryState.SetInventoryState(false);
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
        if (_sentences.Count == 0 && _names.Count == 0)
        {
            _names.Clear();
            _sentences.Clear();
            EndDialogue();
            return;
        }

        //throw exception when the name or sentence is empty
        if (_names.Count != 0)
        {
            nameText.text = _names.Dequeue();;
        }
        
        var sentence = _sentences.Dequeue();
        //if (_sentencesAnimation != null) StopCoroutine(_sentencesAnimation); 
        StopAllCoroutines(); //To avoid on creating another instance of AutoAdvance
        _sentencesAnimation = StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueTMP.text = "";
        
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueTMP.text += letter;
            yield return new WaitForSeconds(_typespeed);
        }

        StartCoroutine(AutoAdvance());
    }

    private IEnumerator AutoAdvance()
    {
        yield return new WaitForSeconds(_autoadvancespeed);
        DisplayNextSentence();
    }

    private void EndDialogue() 
    {
        animator.SetBool(IsOpen, false);
        _inventoryState.SetInventoryState(true);
        if (!_startPointRef) return;
        Triggerer tr = _startPointRef.GetComponent<Triggerer>(); 
        if (tr) tr.Trigger();
    }
}
