using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMain : MonoBehaviour {

    public InventoryObject inventory;
    public List<QuestLock> locks = new List<QuestLock>();
    public int state = 0;

    void Update() {
        foreach (QuestLock qlock in locks) {
            if (qlock.state == state || 
                    (qlock.activeUntilState && state < qlock.state)) {
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
    }

    public void AddAdvancer (QuestLock qa) {
        // called by locks in the scene
        locks.Add(qa);
        if (qa.state != state) { qa.gameObject.SetActive(false); }
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

    public void NextState(QuestLock ql) {
        if (CheckRequirements(ql)) {
            RemoveItems(ql);
            state = ql.nextState;
        }
    }
}
