using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour {

    public bool autoTrigger = false;
    public bool findComponents = false;
    public bool destroyAfterTrigger = false;

    public QuestLock qlock;
    public ItemPickup pickup;
    public DialogueTrigger dialogue;
    public Triggerer triggerer;


    void Start() {
        if (findComponents) {
            if (!qlock) qlock = GetComponent<QuestLock>();
            if (!pickup) pickup = GetComponent<ItemPickup>();
            if (!dialogue) dialogue = GetComponent<DialogueTrigger>();
        }
    }

    void Update() {
        if (autoTrigger) {
            // here instead of in start to not break everything 
            // (dialogue has to call start before this)
            Trigger();
            autoTrigger = false;
        }
    }

    public void Trigger() {
        if (qlock) qlock.Advance();
        if (pickup) pickup.GetItems();
        if (dialogue) dialogue.TriggerDialogue();
        if (triggerer) triggerer.Trigger();
        if (destroyAfterTrigger) {
            Destroy(gameObject, 0.02f);
        }
    }
}
