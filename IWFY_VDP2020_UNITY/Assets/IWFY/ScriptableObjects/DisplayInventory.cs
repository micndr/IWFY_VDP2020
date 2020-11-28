using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    [FormerlySerializedAs("X_START")] public int xStart;
    [FormerlySerializedAs("Y_START")] public int yStart;
    [FormerlySerializedAs("X_SPACE_BETWEEN_ITEMS")] public int xSpaceBetweenItems;
    [FormerlySerializedAs("Y_SPACE_BETWEEN_ITEMS")] public int ySpaceBetweenItems;
    [FormerlySerializedAs("NUMBER_OF_COLUMN")] public int numberOfColumn;
    
    
    void Awake()
    {
        CreateDisplay();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CreateDisplay();
    }

   

    private void ClearDisplay()
    {
        var childs = transform.childCount;
        for (var i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    
    public void CreateDisplay()
    {
        ClearDisplay();
        for (var i = 0; i < inventory.itemList.Count; i++)
        {
            var obj = Instantiate(inventory.itemList[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.itemList[i].amount.ToString("n0") + " " + inventory.itemList[i].itemName;
        }
        
        //Da gestire caso in cui l'inventario contiene un item null
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(xStart+ xSpaceBetweenItems*(i% numberOfColumn),yStart+ (-ySpaceBetweenItems*(i/numberOfColumn)), 0f);
    }
}


/*public void UpdateDisplay() //funzione per aggiornare l'inventario
    {
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.itemList[i]))
            {
                itemsDisplayed[inventory.itemList[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.itemList[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.itemList[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.itemList[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.itemList[i], obj);
            }
        }
    }*/