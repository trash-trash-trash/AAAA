using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Inventory inventory;
    public GameObject gridParentObj;
    public GameObject itemSlotPrefab;

    void Awake()
    {
        inventory.AnnounceOpenCloseInventory += OpenCloseInventory;
    }

    void OpenCloseInventory(bool input)
    {
        if (input)
        {
            RefreshUI();
            gridParentObj.SetActive(true);
        }
        else
        {
            gridParentObj.SetActive(false);
        }
    }

    void RefreshUI()
    {    
        foreach (Transform child in gridParentObj.transform)
            Destroy(child.gameObject);
        
        foreach (ItemSO item in inventory.playerItems)
        {
            GameObject slot = Instantiate(itemSlotPrefab, gridParentObj.transform);
            slot.GetComponentInChildren<Image>().sprite = item.icon;
            slot.GetComponentInChildren<TMP_Text>().text = item.itemName;
            slot.SetActive(true);
        }
    }

    void OnDisable()
    {
        inventory.AnnounceOpenCloseInventory -= OpenCloseInventory;
    }
}