using UnityEngine;

public class LevelPart : MonoBehaviour
{
   public MyLevelPart myLevelPart;
   public GameObject prefab;

   public int placeInQueue;
   public Vector3 rotation;

   public Vector3 address;

   private bool occupied = false;
   public bool Occupied
   {
      get => occupied;
      set => occupied = value;
   }
   public GameObject instance;
}

public enum MyLevelPart
{
   Empty,
   Ground,
   Wall,
   Hallway
}
