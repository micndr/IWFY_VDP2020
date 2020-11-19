using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    private GameObject inventory_display;
    private bool inventoryVisibility = true;

    [SerializeField] public bool InventoryOn = true;
    /*public void OnTriggerEnter(Collider other) //how to add an Item to the inventory we get the component ItemToken, then add the ItemObject to our inventory
    {
        var item = other.GetComponent<ItemToken>();
        if (item)
        {
            inventory.AddItem(item.itemToken,1, 666);
            Destroy(other.gameObject);
            Debug.Log("Item added to inventory");
        }
    }*/

    private void Awake()
    {
        inventory_display = GameObject.Find("CanvasInventory").transform.Find("InventoryScreen").gameObject;
    }

    public void setInventoryState(bool set)
    {
        if (set)
        {    
            InventoryOn = true;
        }
        else
        {
            InventoryOn = false;
        }
    }
    void Update()
    {
        if(InventoryOn)
        {
            if (Input.GetKeyDown(KeyCode.I) && inventoryVisibility)
            {
                inventory_display.SetActive(true);
                inventoryVisibility = false;
            }
            else if (Input.GetKeyDown(KeyCode.I) && !inventoryVisibility)
            {
                inventory_display.SetActive(false);
                inventoryVisibility = true;
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.itemList.Clear();
    }
}

