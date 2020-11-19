using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(0,20)]
    public string description;
    public Dialogue dialogue;

    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
    }

    public void OnEnable()
    {
        TriggerDialogue();
    }
}
