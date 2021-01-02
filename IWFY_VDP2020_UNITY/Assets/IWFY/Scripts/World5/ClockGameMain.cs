using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGameMain : MonoBehaviour {

    ClockSelect[] cls;

    public int[] correctState = {
        0, 3, 0, 0, 2, 0, 1, 0, 0, 0, 4, 0
    };

    void Start() {
        cls = FindObjectsOfType<ClockSelect>();
    }

    bool Check () {
        foreach (ClockSelect cl in cls) {
            if (correctState[int.Parse(cl.transform.parent.name)-1] != cl.selectedValue) {
                return false;
            }
        }
        return true;
    }

    public void OnSelection () {
        if (Check()) {
            GetComponent<Triggerer>().Trigger();
        }
    }

    void Update() {
    }
}
