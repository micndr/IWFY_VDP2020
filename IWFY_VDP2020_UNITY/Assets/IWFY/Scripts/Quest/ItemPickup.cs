using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public QuestMain main;
    public List<ItemObject> pickups = new List<ItemObject>();

    public int keyNum = -1;

    void Start() {
        main = GameObject.Find("QuestMain").GetComponent<QuestMain>();
    }

    // called by a triggerer, it add to the inventory every item in pickups.
    public void GetItems () {
        foreach (ItemObject item in pickups) {
            main.inventory.AddItem(item, 1, item.itemID, item.itemName);
        }

        if (keyNum != -1) {
            GlobalState gs = FindObjectOfType<GlobalState>();
            gs.keys[keyNum] = 1;
        }
    }
}
