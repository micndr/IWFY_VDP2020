using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
    
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    //public event EventHandler OnInventoryChange;
    public List<InventorySlot> itemList = new List<InventorySlot>(); //Storing items in a list

    public void AddItem(ItemObject _item, int _amount, int _itemID, string _itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item == _item)
            {
                itemList[i].AddAmount(_amount);
                return;
            }
        }
        itemList.Add(new InventorySlot(_item,_amount,_itemID, _itemName));
    }
    public void AddItem(ItemObject _item, int _amount, int _itemID)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item == _item)
            {
                itemList[i].AddAmount(_amount);
                return;
            }
        }
        itemList.Add(new InventorySlot(_item,_amount,_itemID));
        //OnInventoryChange?.Invoke(this, EventArgs.Empty);
    }
    

    /*public void RemoveItem()
    {
        //itemList.RemoveAt(0);//removing the first item on the list
        //I can set an ID for the item and remove it by ID!
    }*/
    public void RemoveItem(int _itemID)
    {
        foreach (var item in itemList)
        {
            if (item.itemID == _itemID) //removing all item containing that ID!
            {
                itemList.Remove(item);
                
                Debug.Log("Successfully removed the item");
                return;
            }
            
            Debug.Log("Step");
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public int itemID;
    public string itemName;
    
    public InventorySlot(ItemObject _item, int _amount, int _itemID, string _itemName)
    {
        item = _item;
        amount = _amount;
        itemID = _itemID;
        itemName = _itemName;
    }
    public InventorySlot(ItemObject _item, int _amount, int _itemID)
    {
        item = _item;
        amount = _amount;
        itemID = _itemID;
    }
    

    public void AddAmount(int value)
    {
        amount += value;
    }
}
