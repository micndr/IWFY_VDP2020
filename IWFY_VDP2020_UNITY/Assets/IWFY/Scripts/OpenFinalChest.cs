using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalChest : MonoBehaviour {

    public Animator anim;
    public GameObject collider;

    void Trigger() {
        GlobalState gs = FindObjectOfType<GlobalState>();
        if (gs.completedQuests.Contains("Chest")) { return; }
        int sum = 0; foreach (int n in gs.keys) sum += n;
        if (sum == 4) {
            anim.SetTrigger("trigger");
            collider.SetActive(false);
            gs.completedQuests.Add("Chest");
        }
    }
}
