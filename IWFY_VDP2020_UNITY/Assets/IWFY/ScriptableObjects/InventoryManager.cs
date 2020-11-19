using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    private GameObject _inventoryDisplay;
    private bool _inventoryVisibility = true;

    [FormerlySerializedAs("InventoryOn")] [SerializeField] public bool inventoryOn = true;
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
        _inventoryDisplay = GameObject.Find("CanvasInventory").transform.Find("InventoryScreen").gameObject;
    }

    public void SetInventoryState(bool set)
    {
        inventoryOn = set;
    }
    void Update()
    {
        if (!inventoryOn) return;
        if (Input.GetKeyDown(KeyCode.I) && _inventoryVisibility)
        {
            _inventoryDisplay.SetActive(true);
            _inventoryVisibility = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !_inventoryVisibility)
        {
            _inventoryDisplay.SetActive(false);
            _inventoryVisibility = true;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.itemList.Clear();
    }
}

