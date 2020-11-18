using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// forbids access to this element if the global quest state
// is not equal to this element's state

public class QuestLock : MonoBehaviour {

    public QuestMain main;
    public int state = 0;
    public int nextState = 0;

    public List<ItemObject> requirements = new List<ItemObject>();

    public int deactivateDelay = 0;

    void Start() {
        main = GameObject.Find("QuestMain").GetComponent<QuestMain>();
        main.AddAdvancer(this);
    }

    public void Advance () {
        main.NextState(this);
    }
}
