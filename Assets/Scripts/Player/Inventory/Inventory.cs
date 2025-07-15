using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public bool inventoryOpen = false;
   public bool canOpenInventory = true;
   public List<ItemSO> playerItems = new List<ItemSO>();

   public PlayerInputHandler playerInputs;
   public PlayerMovementHandler playerMovementHandler;

   public event Action<bool> AnnounceOpenCloseInventory;

   public void Awake()
   {
      playerInputs.AnnounceInventory += OpenCloseInventory;
   }

   //opening inventory stops the player from being able to move
   //this is optional, implementing for now to make it feel more strict/constrained for that horror feeling
   private void OpenCloseInventory(bool input)
   {
      if (!canOpenInventory)
         return;

      if (input)
      {
         if (!inventoryOpen)
         {
            playerMovementHandler.CanMove = false;
            inventoryOpen = true;
            AnnounceOpenCloseInventory?.Invoke(true);
         }
         else
         {
            playerMovementHandler.CanMove = true;
            inventoryOpen = false;
            AnnounceOpenCloseInventory?.Invoke(false);
         }
      }
   }

   public void AddItem(ItemSO newItem)
   {
      playerItems.Add(newItem);
   }

   public void RemoveItem(ItemSO newItem)
   {
      if (playerItems.Contains(newItem))
         playerItems.Remove(newItem);
   }

   void OnDisable()
   {
      playerInputs.AnnounceInventory -= OpenCloseInventory;
   }
}
