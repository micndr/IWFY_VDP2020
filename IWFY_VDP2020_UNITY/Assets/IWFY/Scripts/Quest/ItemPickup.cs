using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public QuestMain main;
    public List<ItemObject> pickups = new List<ItemObject>();

    void Start() {
        main = GameObject.Find("QuestMain").GetComponent<QuestMain>();
    }

    public void GetItems () {
        foreach (ItemObject item in pickups) {
            main.inventory.AddItem(item, 1, item.itemID, item.itemName);
        }
    }
}
