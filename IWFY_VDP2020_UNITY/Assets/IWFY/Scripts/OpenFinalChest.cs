using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalChest : MonoBehaviour {

    public Animator anim;
    public GameObject collider;
    public DialogueTrigger dialogue;
    public Outline outline;

    void TriggerMsg() {
        GlobalState gs = FindObjectOfType<GlobalState>();
        if (gs.completedQuests.Contains("Chest")) { return; }
        int sum = 0; foreach (int n in gs.keys) sum += n;
        if (sum == 4) {
            anim.SetTrigger("trigger");
            collider.SetActive(false);
            gs.completedQuests.Add("Chest");
            if (outline) outline.enabled = false;
        } else {
            if (dialogue) dialogue.TriggerDialogue();
        }
    }
}
