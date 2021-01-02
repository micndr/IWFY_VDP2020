using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// all the quest logic
public class QuestMain : MonoBehaviour {

    GlobalState globalState; /* has settings and completed quest list */

    public string questName;

    public InventoryObject inventory; /* player's inventory */

    // locks subscribe on their own
    public List<QuestLock> locks = new List<QuestLock>();
    public int state = 0; /* this controls which locks are active */

    public string[] stateNames; /* the topleft tips */
    public bool completed = false; 
    
    // list of quest that are necessary to start this one
    public List<string> questRequirements = new List<string>();

    // internal
    public int lenght = 0; /* largest lock.nextstate */
    Text QuestText;
    private bool resetQuestText = true;
    private Compass compass;

    private void Start() {
        // cache components and references
        if (questName.Length == 0 && stateNames.Length > 0) { questName = stateNames[0]; }
        QuestText = GameObject.Find("QuestText").GetComponent<Text>();
        globalState = FindObjectOfType<GlobalState>();

        GameObject compassObj = GameObject.Find("CompassPrefab");
        if (compassObj) compass = compassObj.GetComponent<Compass>();
    }

    public bool CheckQuestLockState(QuestLock qlock) {
        // condition for lock's activation or deactivation
        bool check = qlock.state == state
                || (qlock.activeUntilState && state < qlock.state);
        if (qlock.invert) check = !check;
        return check;
    }

    void Update() {
        if (!CheckQuestRequirements()) {
            // if globalstate boes not have the required completed quests, deactivate all locks.
            foreach (QuestLock qlock in locks) {
                qlock.gameObject.SetActive(false);
            }
            return;
        }

        if (!completed) {
            if (stateNames.Length > state) {
                // assign to the tip text the according tip based on state.
                QuestText.text = stateNames[state];
            } else {
                Debug.LogWarning("set name to states in QuestMain plz");
                stateNames = new string[stateNames.Length + 1];
                stateNames[stateNames.Length - 1] = "No Current Objective";
            }

            if (compass) {
                // search for a lock that wants a compass, if found, set compass target to that one.
                // if not found, target is null, so it deactivates the compass.
                QuestLock target = null;
                foreach (QuestLock l in locks) {
                    if (CheckQuestLockState(l)) {
                        if (l.toCompass) {
                            target = l;
                            break;
                        }
                    }
                }
                if (target) compass.target = target.transform;
            }

        } else {
            // in order not to mess with the assignment to the tip text
            // when completed, assign one last time.
            if (resetQuestText) {
                resetQuestText = false;
                compass.target = null;
                QuestText.text = stateNames[stateNames.Length-1];
            }
        }

        // main activation/deactivation logic.
        foreach (QuestLock qlock in locks) {
            if (CheckQuestLockState(qlock)) {
                // if the condition is satisfied, the lock is active.
                qlock.gameObject.SetActive(true);
            } else {
                // otherwise, wait two frames, then deactivate it.
                // the delay is to the components to clean up and not call stuff 
                // on a deactivated gameobject.
                if (qlock.gameObject.activeSelf) {
                    if (qlock.deactivateDelay < 2) {
                        qlock.deactivateDelay += 1;
                    } else {
                        qlock.gameObject.SetActive(false);
                        qlock.deactivateDelay = 0;
                    }
                } else {
                    // it is then set to false every frame.
                    qlock.gameObject.SetActive(false);
                }
            }
        }

        // only in the unity editor, skip the progression with n
        if (Input.GetKeyDown(KeyCode.N) && state < lenght && Application.isEditor) {
            state++;
            CheckCompletion();
        }
    }

    public void AddAdvancer (QuestLock qa) {
        // called by locks in the scene
        locks.Add(qa);
        if (qa.state != state) { qa.gameObject.SetActive(false); }

        if (qa.nextState >= lenght) {
            lenght = qa.nextState;
            if (completed) { state = lenght; }
        }
    }
    public void RemoveAdvancer(QuestLock qa) {
        // called by locks in the scene
        locks.Remove(qa);
    }

    public bool CheckRequirements (QuestLock ql) {
        // it seems O(n^3), it's fine it's not called every frame on small lists.
        var checkd = new List<int>();
        bool flag = true;
        // for every lock's item requirement
        for (int j = 0; j < ql.requirements.Count; j++) {
            // search the item from the inventory
            bool found = false;
            for (int i = 0; i < inventory.itemList.Count; i++) {
                if (inventory.itemList[i].item.itemID == ql.requirements[j].itemID) {
                    int itemcount = inventory.itemList[i].amount;
                    /* in order to account for the quantity of the items a temp list
                     * is keeping track of how many have been found.
                     */
                    for (int k = 0; k < checkd.Count; k++) {
                        if (checkd[k] == i) { itemcount--; }
                    }
                    if (itemcount > 0) {
                        found = true;
                        checkd.Add(i);
                    }
                }
            }
            // if not found, requirements are not met and the result is false.
            if (!found) { flag = false; break; }
        }
        // if all are found, flag is still true
        return flag;
    }

    public void RemoveItems (QuestLock ql) {
        for (int j = 0; j < ql.requirements.Count; j++) {
            inventory.RemoveItem(ql.requirements[j].itemID);
        }
    }

    public void CheckCompletion() {
        // lenght is largest lock.nextstate, so when it is reached quest is completed
        if (state >= lenght) {
            completed = true;
            if (!globalState) globalState = FindObjectOfType<GlobalState>();
            if (!globalState) {
                /* (only if the globalstate is missing, make one) */
                Debug.LogWarning("game was not started from hub! making new globalstate instance");
                GameObject gs = GameObject.Instantiate(new GameObject());
                globalState = gs.AddComponent<GlobalState>();
                globalState.GetComponent<GlobalState>().completedQuests.Add("Tutorial");
                if (questName == "Pixie")
                    globalState.GetComponent<GlobalState>().completedQuests.Add("Chasm");
            } 
            // and added to the globalstate
            globalState.AddQuest(this);
        }
    }

    public bool CheckQuestRequirements() {
        // check if globalState.completedQuests contains all questrequirements
        foreach (string req in questRequirements) {
            bool found = false;
            foreach (string completed in globalState.completedQuests) {
                if (completed == req) { found = true; break; }
            }
            if (!found) return false;
        }
        return true;
    }

    public void NextState(QuestLock ql) {
        // called by lock.advance
        if (CheckRequirements(ql)) {
            RemoveItems(ql);
            state = ql.nextState;
            CheckCompletion();
            if (compass) compass.target = null;
        }
    }
}
