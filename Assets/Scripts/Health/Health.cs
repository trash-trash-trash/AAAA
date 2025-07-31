using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isAlive;

    public int currentHealth;
    public int maxHealth = 1;
    private int minHealth = 0;

    public event Action<bool> AnnounceIsAlive;
    
    public void Kill()
    {
        ChangeHealth(-666);
    }

    public void Rez()
    {
        ChangeHealth(maxHealth);
    }

    public void ChangeHealth(int amount)
    {        
        currentHealth += amount;
      
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);

        isAlive = currentHealth > minHealth;
        
        AnnounceIsAlive?.Invoke(isAlive);
    }
}
