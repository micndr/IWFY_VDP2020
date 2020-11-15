using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    
    //Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>(); //Not really used
    // Start is called before the first frame update
    void Awake()
    {
        CreateDisplay();
        gameObject.SetActive(false);
        //inventory.RemoveItem(666); //method to remove an Item by its ID from the inventory and destroy it
    }

    private void OnEnable()
    {
        clearDisplay();
        CreateDisplay();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            clearDisplay();
            
            //Destroy(transform.GetChild(0).gameObject);
            //Debug.Log("Ho distrutto un figlio");
            
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            clearDisplay();
            CreateDisplay();
        }
        
    }*/

    private void clearDisplay()
    {
        var childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            var obj = Instantiate(inventory.itemList[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.itemList[i].amount.ToString("n0");
        }
        
        //Da gestire caso in cui l'inventario contiene un item null
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START+ X_SPACE_BETWEEN_ITEMS*(i% NUMBER_OF_COLUMN),Y_START+ (-Y_SPACE_BETWEEN_ITEMS*(i/NUMBER_OF_COLUMN)), 0f);
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