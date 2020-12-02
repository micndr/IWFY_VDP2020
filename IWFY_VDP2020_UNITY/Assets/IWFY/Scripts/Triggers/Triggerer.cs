﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : MonoBehaviour {

    public bool autoTrigger = false;
    public float autoTriggerDelay;
    float autoTriggerTimer;
    public bool findComponents = false;
    public bool destroyAfterTrigger = false;
    public bool disableAfterTrigger = false; //Deactivates the trigger without destroying the whole object, enabled means it only does the first interaction
    private bool _triggered = false;
    
    public QuestLock qlock;
    public ItemPickup pickup;
    public DialogueTrigger dialogue;
    public Triggerer triggerer;
    public Animator animator;

    public MirrorController mirrorcont;
    public ropeActivator ropeActivator;

    public AudioSource audioSource;
    public FxThunderTrigger thunder;
    public OpenLink link;
    public videoController video;

    public bool lockPlayer = false;
    public bool unlockPlayer = false;
    PlayerMove playerMove;

    void Start() {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

        if (findComponents) {
            if (!qlock) qlock = GetComponent<QuestLock>();
            if (!pickup) pickup = GetComponent<ItemPickup>();
            if (!dialogue) dialogue = GetComponent<DialogueTrigger>();
            if (!animator) animator = GetComponent<Animator>();
            if (!audioSource) audioSource = GetComponent<AudioSource>();
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
        if (_triggered) {
            return;
        }

        /*
        // triggers this function after triggerdelay seconds
        if (autoTriggerDelay > 0 && !autoTrigger) {
            autoTrigger = true;
            autoTriggerTimer = Time.time + autoTriggerDelay;
        }*/

        if (qlock) qlock.Advance();
        if (pickup) pickup.GetItems();
        if (dialogue) dialogue.TriggerDialogue();
        if (triggerer) triggerer.Trigger();
        if (animator) animator.SetTrigger("trigger");
        if (mirrorcont) mirrorcont.RotateMirror();
        if (audioSource) audioSource.Play();
        if (thunder) thunder.Strike();
        if (link) link.Open();
        if (video) video.Play();
        if (lockPlayer) playerMove.lockUserInput = true;
        if (unlockPlayer) playerMove.lockUserInput = false;
        if (ropeActivator) ropeActivator.activateRope();
        if (destroyAfterTrigger) {
            Destroy(gameObject, 0.02f);
        }
        if (disableAfterTrigger)
        {
            _triggered = true;
        }
        
    }
}
