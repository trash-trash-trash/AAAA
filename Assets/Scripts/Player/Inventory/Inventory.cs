using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public bool inventoryOpen = false;
   public bool canOpenInventory = true;
   
   //reset between open/close or remain consistent?
   public int selectIndex = 0;
   [SerializeField]
   public ItemSO selectedItem;
   public List<ItemSO> playerItems = new List<ItemSO>();

   public PlayerInputHandler playerInputs;

   public event Action<bool> AnnounceOpenCloseInventory;
   public event Action<int> AnnounceSelectIndex;

   public void Awake()
   {
      playerInputs.AnnounceInventory += OpenCloseInventory;
      playerInputs.AnnounceMoveVector2 += ScrollInventory;
   }

   //opening inventory stops the player from being able to move
   //this is optional, implementing for now to make it feel more strict/constrained for that horror feeling
   private void OpenCloseInventory(bool input)
   {
      if (!canOpenInventory)
         return;

      if (input)
      {
         if (!inventoryOpen && playerItems.Count > 0)
         {
            inventoryOpen = true;
            SelectItem(selectIndex);
            AnnounceOpenCloseInventory?.Invoke(true);
         }
         else
         {
            inventoryOpen = false;
            AnnounceOpenCloseInventory?.Invoke(false);
         }
      }
   }

   private void SelectItem(int index)
   {
      if (playerItems.Count == 0)
         return;

      selectIndex = Mathf.Clamp(index, 0, playerItems.Count - 1);
      selectedItem = playerItems[selectIndex];
      AnnounceSelectIndex?.Invoke(selectIndex);
   }
   
   private void ScrollInventory(Vector2 input)
   {
      if (!inventoryOpen || playerItems.Count == 0)
         return;
      
      int direction = 0;

      if (input.x > 0.5f || input.y > 0.5f)
         direction = 1;
      else if (input.x < -0.5f || input.y < -0.5f)
         direction = -1;

      if (direction != 0)
      {
         selectIndex += direction;

         if (selectIndex >= playerItems.Count)
            selectIndex = 0;
         else if (selectIndex < 0)
            selectIndex = playerItems.Count - 1;

         SelectItem(selectIndex);
      }
   }

   public void AddItem(ItemSO newItem)
   {
      playerItems.Add(newItem);
   }

   public void LeftButton()
   {
      int newIndex = selectIndex - 1;
      if (newIndex < 0)
         newIndex = playerItems.Count - 1;

      SelectItem(newIndex);
   }

   public void RightButton()
   {
      int newIndex = selectIndex + 1;
      if (newIndex >= playerItems.Count)
         newIndex = 0;

      SelectItem(newIndex);
   }


   public void RemoveItem(ItemSO newItem)
   {
      if (playerItems.Contains(newItem))
         playerItems.Remove(newItem);
      
      if (selectIndex >= playerItems.Count)
         selectIndex = Mathf.Max(0, playerItems.Count - 1);

      SelectItem(selectIndex);
   }

   public void Reset()
   {
      List<ItemSO> itemsToRemove = new List<ItemSO>(playerItems);
      foreach (var item in itemsToRemove)
      {
         item.Reset();
         RemoveItem(item);
      }
      playerItems.Clear();
      AnnounceOpenCloseInventory?.Invoke(false);
   }

   void OnDisable()
   {
      playerInputs.AnnounceInventory -= OpenCloseInventory;
   }
}
