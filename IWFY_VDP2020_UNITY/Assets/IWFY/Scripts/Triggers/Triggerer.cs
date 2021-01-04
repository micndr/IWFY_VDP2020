using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Triggerer : MonoBehaviour {

    public bool autoTrigger = false;
    public float autoTriggerDelay;
    float autoTriggerTimer;
    public bool destroyAfterTrigger = false;
    public bool disableAfterTrigger = false; //Deactivates the trigger without destroying the whole object, enabled means it only does the first interaction
    private bool _triggered = false;
    public int playAmbientSound = -1;
    public int stopAmbientSound = -1;

    public bool findComponents = false;
    public QuestLock qlock;
    public ItemPickup pickup;
    public DialogueManager dialogueManager;
    public DialogueTrigger dialogue;
    public Triggerer triggerer;
    public Animator animator;

    public MirrorController mirrorcont;
    public bidoneController bidoneController;
    public ropeActivator ropeActivator;

    public AudioSource audioSource;
    public FxThunderTrigger thunder;
    public OpenLink link;
    public videoController video;
    public openNote openNote;

    public Component component;

    public bool lockPlayer = false;
    public bool unlockPlayer = false;
    PlayerMove playerMove;
    MoveFlat moveFlat;
    AudioManager audioManager;

    void Start() {
        //playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        moveFlat = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveFlat>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        if (findComponents) {
            if (!qlock) qlock = GetComponent<QuestLock>();
            if (!pickup) pickup = GetComponent<ItemPickup>();
            if (!dialogue) dialogue = GetComponent<DialogueTrigger>();
            if (!animator) animator = GetComponent<Animator>();
            if (!audioSource) audioSource = GetComponent<AudioSource>();
        }

        autoTriggerTimer = Time.time + autoTriggerDelay;

        if (playAmbientSound != -1) { audioManager = FindObjectOfType<AudioManager>(); }
        if (stopAmbientSound != -1) { audioManager = FindObjectOfType<AudioManager>(); }
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

    private void OnEnable() {
        autoTriggerTimer = Time.time + autoTriggerDelay;
    }

    public void Trigger() {
        if (_triggered) {
            return;
        }

        print("lol");

        /*
        // triggers this function after triggerdelay seconds
        if (autoTriggerDelay > 0 && !autoTrigger) {
            autoTrigger = true;
            autoTriggerTimer = Time.time + autoTriggerDelay;
        }*/

        if (qlock) qlock.Advance();
        if (pickup) pickup.GetItems();
        if (dialogue) 
        {
            if (dialogueManager.isInConversation)
            {
                return;
            }
            dialogue.TriggerDialogue();
            
        }
        if (triggerer) triggerer.Trigger();
        if (animator) {
            animator.SetTrigger("trigger");
        }
        if (mirrorcont) mirrorcont.RotateMirror();
        if (bidoneController) bidoneController.RotateBidone();
        if (audioSource) audioSource.Play();
        if (thunder) thunder.Strike();
        if (link) link.Open();
        if (video) video.Play();
        if (lockPlayer) {
            //playerMove.lockUserInput = true;
            moveFlat.lockUserInput = true;
        }
        if (unlockPlayer) {
            //playerMove.lockUserInput = false;
            moveFlat.lockUserInput = false;
        }
        if (ropeActivator) ropeActivator.activateRope();
        if (openNote) openNote.showMessage();
        if (playAmbientSound != -1) { audioManager.PlayAmbient(playAmbientSound); }
        if (stopAmbientSound != -1) { audioManager.StopAmbient(stopAmbientSound); }
        if (component) component.SendMessage("Trigger", SendMessageOptions.DontRequireReceiver);
        if (destroyAfterTrigger) {
            Destroy(gameObject, 0.02f);
        }
        if (disableAfterTrigger)
        {
            _triggered = true;
            /*if (GetComponent<IwfyClickableObjectNoPopup>() != null)
            {
                GetComponent<IwfyClickableObjectNoPopup>().enabled = false;
            }*/
            
            if (GetComponent<Outline>() != null)
            {
                Destroy(GetComponent<Outline>()); 
            }

            IwfyClickableObjectNoPopup[] allChildrenandSelf = GetComponentsInChildren<IwfyClickableObjectNoPopup>();
            foreach (IwfyClickableObjectNoPopup child in allChildrenandSelf)
            {
                Destroy(child);
            }
            
        }
        
    }
}
