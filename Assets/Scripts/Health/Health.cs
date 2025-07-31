using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isAlive;

    public int currentHealth;
    public int maxHealth = 1;
    private int minHealth = 0;
    
    public event Action<int> AnnounceCurrentHealth;
    public event Action<bool> AnnounceIsAlive;
    
    public virtual void Kill()
    {
        ChangeHealth(-666);
    }

    public virtual void Rez()
    {
        ChangeHealth(maxHealth);
    }

    public virtual void ChangeHealth(int amount)
    {        
        currentHealth += amount;
      
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);

        isAlive = currentHealth > minHealth;
        
        AnnounceIsAlive?.Invoke(isAlive);
        AnnounceCurrentHealth?.Invoke(currentHealth);
    }

    public virtual void ChangeMaxHealth(int amount)
    {
        maxHealth += amount;
        ChangeHealth(currentHealth);
    }
}
