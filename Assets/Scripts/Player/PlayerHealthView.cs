using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
   public Health health;

   public TMP_Text healthText;

   public bool testing = true;

   public GameObject youDiedObj; 
   public float fadeDuration = 5f;
   public TMP_Text text;
   private Color originalColor;

   void OnEnable()
   {
      health.AnnounceCurrentHealth += SetHPUI;
      health.AnnounceIsAlive += YouDied;
      SetHPUI(health.maxHealth);
   }

   private void YouDied(bool alive)
   {
      if(!alive)
       StartCoroutine(FadeText());
   }

   IEnumerator FadeText()
   {
      youDiedObj.SetActive(true);

      // Reset alpha
      text.color = originalColor;

      float elapsed = 0f;

      while (elapsed < fadeDuration)
      {
         elapsed += Time.deltaTime;
         float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

         Color c = text.color;
         c.a = alpha;
         text.color = c;

         yield return null;
      }

      // ensure fully transparent
      Color finalColor = text.color;
      finalColor.a = 0f;
      text.color = finalColor;

      youDiedObj.SetActive(false);

      text.color = originalColor;
   }

   private void SetHPUI(int obj)
   {
      if(testing)
      {
         string hpText = obj.ToString("D2");
         healthText.text = "HP: "+hpText;
      }
   }

   private void OnDisable()
   {
      health.AnnounceIsAlive -= YouDied;
      health.AnnounceCurrentHealth -= SetHPUI;
   }
}
