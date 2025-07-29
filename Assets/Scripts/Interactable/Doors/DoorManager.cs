using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
   //seemsbad
   
   public List<Door> allDoors = new List<Door>();
   
   public void ResetDoors()
   {
      foreach (Door door in allDoors)
      {
         door.ResetDoor();
      }
   }
}
