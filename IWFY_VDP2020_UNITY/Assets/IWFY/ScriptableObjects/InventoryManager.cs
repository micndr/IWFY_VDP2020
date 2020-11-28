using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject _inventoryDisplay;
    public GameObject _backpackText;
    public bool _inventoryOff = true;

    [FormerlySerializedAs("InventoryOn")] [SerializeField] public bool inventoryOn = true;
    

    private void Awake()
    {
        _inventoryDisplay = GameObject.Find("CanvasInventory").transform.Find("InventoryScreen").gameObject;
        _backpackText = GameObject.Find("CanvasInventory").transform.Find("Backpack text").gameObject;
        _backpackText.SetActive(false);
    }

    public void SetInventoryState(bool set)
    {
        inventoryOn = set;
    }
    void Update()
    {
        if (!inventoryOn) return;
        if (Input.GetKeyDown(KeyCode.I) && _inventoryOff) //
        {
            _inventoryDisplay.SetActive(true);
            _backpackText.SetActive(true);
            _inventoryOff = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !_inventoryOff)
        {
            _inventoryDisplay.SetActive(false);
            _backpackText.SetActive(false);
            _inventoryOff = true;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.itemList.Clear();
    }
}


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
