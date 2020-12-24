using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// disables this element if the global quest != this element's state
public class QuestLock : MonoBehaviour {

    public QuestMain main; /* optional, if null it searches for one (mandatory if 2 in the scene) */
    public int state = 0; /* mandatory */
    public int nextState = 0; /* optional, used when advance is called */
    // activeUntilState and invert modify the condition for disabling
    public bool activeUntilState = false;
    public bool invert = false;
    public bool toCompass = false; /* if true, the compass points to this gameobject */

    // if the invetory doesn't have all the requirements, Advance fails.
    public List<ItemObject> requirements = new List<ItemObject>();

    public int deactivateDelay = 0;

    void Start() {
        if (!main) {
            main = GameObject.Find("QuestMain").GetComponent<QuestMain>();
        }
        // subscribe to the QuestMain
        main.AddAdvancer(this);
    }

    public void Advance () {
        // called by triggerer, assigns globalstate to this.nextstate
        main.NextState(this);
    }

    public void OnDestroy() {
        // used to cleanup after triggerer.destroyaftertrigger
        if (main) {
            main.RemoveAdvancer(this);
        }
    }
}
