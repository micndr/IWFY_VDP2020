using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour {

    public bool autoTrigger = false;
    public float autoTriggerDelay;
    float autoTriggerTimer;
    public bool findComponents = false;
    public bool destroyAfterTrigger = false;

    public QuestLock qlock;
    public ItemPickup pickup;
    public DialogueTrigger dialogue;
    public Triggerer triggerer;

    public MirrorController mirrorcont;


    void Start() {
        if (findComponents) {
            if (!qlock) qlock = GetComponent<QuestLock>();
            if (!pickup) pickup = GetComponent<ItemPickup>();
            if (!dialogue) dialogue = GetComponent<DialogueTrigger>();
        }

        autoTriggerTimer = Time.time + autoTriggerDelay;
    }

    void Update() {
        if (autoTrigger) {
            if (autoTriggerTimer <= Time.time) {
                // here instead of in start to not break everything 
                // (dialogue has to call start before this)
                Trigger();
                autoTrigger = false;
                autoTriggerTimer = Time.time + autoTriggerDelay;
            }
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
        if (mirrorcont) mirrorcont.RotateMirror();
    }
}
