using System;
using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
   public Health health;

   public TMP_Text healthText;

   public bool testing = true;

   void Start()
   {
      health.AnnounceCurrentHealth += SetHPUI;
   }

   private void SetHPUI(int obj)
   {
      if(testing)
         healthText.text = obj.ToString("D2");
   }

   private void OnDisable()
   {
      health.AnnounceCurrentHealth -= SetHPUI;
   }
}
