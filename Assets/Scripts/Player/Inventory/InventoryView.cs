using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Inventory inventory;
    public GameObject scrollPanelObj;
    public GameObject gridParentObj;
    public GameObject itemSlotPrefab;

    public ScrollRect scrollRect;
    public RectTransform viewport;
    public RectTransform content;

    void Awake()
    {
        inventory.AnnounceOpenCloseInventory += OpenCloseInventory;
        inventory.AnnounceSelectIndex += ScrollToItem;
    }

    private void ScrollToItem(int index)
    {
        if (inventory.playerItems.Count == 0 || index < 0 || index >= content.childCount || viewport == null)
            return;

        RectTransform targetItem = content.GetChild(index) as RectTransform;
        if (targetItem == null)
            return;

        // Get the center position of the target item in content space
        Vector2 localPosition = content.InverseTransformPoint(targetItem.position);
        Vector2 viewportLocal = content.InverseTransformPoint(viewport.position);

        // Calculate offset to move content so target is centered
        Vector2 targetOffset = localPosition - viewportLocal;

        // Only care about horizontal offset
        float newX = content.anchoredPosition.x - targetOffset.x;

        // Clamp content movement within scrollable bounds
        float contentWidth = content.rect.width;
        float viewportWidth = viewport.rect.width;
        float maxX = 0f;
        float minX = viewportWidth - contentWidth;
        newX = Mathf.Clamp(newX, minX, maxX);

        content.anchoredPosition = new Vector2(newX, content.anchoredPosition.y);
    }

    void OpenCloseInventory(bool input)
    {
        if (input)
        {
            RefreshUI();
            scrollPanelObj.SetActive(true);
        }
        else
        {
            scrollPanelObj.SetActive(false);
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
        inventory.AnnounceSelectIndex -= ScrollToItem;
    }
}