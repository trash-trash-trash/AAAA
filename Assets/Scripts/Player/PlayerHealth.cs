using UnityEngine;

public class PlayerHealth : Health
{
    public int normalMaxHealth = 3;
    public int hardMaxHealth = 1;
    
    public override void Rez()
    {
        if (AAAGameManager.Instance.currentDifficulty == Difficulty.Normal)
            maxHealth = normalMaxHealth;
        else
            maxHealth = hardMaxHealth;
        
        base.ChangeHealth(maxHealth);
    }
}
