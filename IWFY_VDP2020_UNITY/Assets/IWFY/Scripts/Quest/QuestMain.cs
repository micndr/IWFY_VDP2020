using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMain : MonoBehaviour {

    GlobalState globalState;

    public string questName;

    public InventoryObject inventory;

    public List<QuestLock> locks = new List<QuestLock>();
    public int state = 0;

    public string[] stateNames;
    public bool completed = false;

    public List<string> questRequirements = new List<string>();

    int lenght = 0;

    Text QuestText;

    private bool resetQuestText = true;

    private void Start() {
        if (questName.Length == 0 && stateNames.Length > 0) { questName = stateNames[0]; }
        QuestText = GameObject.Find("QuestText").GetComponent<Text>();
        globalState = FindObjectOfType<GlobalState>();
    }

    void Update() {
        if (!CheckQuestRequirements()) {
            foreach (QuestLock qlock in locks) {
                qlock.gameObject.SetActive(false);
            }
            return;
        }

        if (!completed) {
            if (stateNames.Length > state) {
                QuestText.text = stateNames[state];
            } else {
                Debug.LogWarning("set name to states in QuestMain plz");
                stateNames = new string[stateNames.Length + 1];
                stateNames[stateNames.Length - 1] = "No Current Objective";
            }
        } else {
            if (resetQuestText) {
                resetQuestText = false;
                QuestText.text = stateNames[stateNames.Length-1];
            }
        }

        foreach (QuestLock qlock in locks) {
            bool check = qlock.state == state 
                || (qlock.activeUntilState && state < qlock.state);
            if (qlock.invert) check = !check;
            if (check) {
                qlock.gameObject.SetActive(true);
            } else {
                if (qlock.gameObject.activeSelf) {
                    if (qlock.deactivateDelay < 2) {
                        qlock.deactivateDelay += 1;
                    } else {
                        qlock.gameObject.SetActive(false);
                        qlock.deactivateDelay = 0;
                    }
                } else {
                    qlock.gameObject.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && state < lenght) {
            state++;
            CheckCompletion();
        }
    }

    public void AddAdvancer (QuestLock qa) {
        // called by locks in the scene
        locks.Add(qa);
        if (qa.state != state) { qa.gameObject.SetActive(false); }

        if (qa.nextState >= lenght)
        {
            lenght = qa.nextState;
            //Debug.Log(qa.nextState);
        }
    }
    public void RemoveAdvancer(QuestLock qa) {
        // called by locks in the scene
        locks.Remove(qa);
    }

    public bool CheckRequirements (QuestLock ql) {
        var checkd = new List<int>();
        bool flag = true;
        for (int j = 0; j < ql.requirements.Count; j++) {
            bool found = false;
            for (int i = 0; i < inventory.itemList.Count; i++) {
                if (inventory.itemList[i].item.itemID == ql.requirements[j].itemID) {
                    int itemcount = inventory.itemList[i].amount;
                    for (int k = 0; k < checkd.Count; k++) {
                        if (checkd[k] == i) { itemcount--; }
                    }
                    if (itemcount > 0) {
                        found = true;
                        checkd.Add(i);
                    }
                }
            }
            if (!found) { flag = false; }
        }
        return flag;
    }

    public void RemoveItems (QuestLock ql) {
        for (int j = 0; j < ql.requirements.Count; j++) {
            inventory.RemoveItem(ql.requirements[j].itemID);
        }
    }

    public void CheckCompletion() {
        if (state >= lenght) {
            completed = true;
            if (!globalState) globalState = FindObjectOfType<GlobalState>();
            if (!globalState) {
                // REMOVE ON DEPLOY
                Debug.LogWarning("game was not started from hub! making new globalstate instance");
                GameObject gs = GameObject.Instantiate(new GameObject());
                globalState = gs.AddComponent<GlobalState>();
                globalState.GetComponent<GlobalState>().completedQuests.Add("Tutorial");
                if (questName == "Pixie")
                    globalState.GetComponent<GlobalState>().completedQuests.Add("Chasm");
            } 
            globalState.AddQuest(this);
        }
    }

    public bool CheckQuestRequirements() {
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
        if (CheckRequirements(ql)) {
            RemoveItems(ql);
            state = ql.nextState;
            CheckCompletion();
        }
    }
}
