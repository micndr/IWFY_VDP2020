using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKey : MonoBehaviour {

    public Triggerer triggerer;
    public KeyCode keyTrigger;

    void Update() {

        if (keyTrigger != KeyCode.None) {
            if (Input.GetKeyDown(keyTrigger)) { triggerer.Trigger(); }
        }
    }
}
