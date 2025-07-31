using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseCase : MonoBehaviour
{
    public ItemSO healthUpISO;
    public ItemSO maxHealthUpISO;

    public Health health;

    private Dictionary<ItemSO, Action> itemUseCaseDict = new();

    void Start()
    {
        itemUseCaseDict.Add(healthUpISO, HealthUp);
        itemUseCaseDict.Add(maxHealthUpISO, MaxHealthUp);
    }

    public void Use(ItemSO item)
    {
        if (itemUseCaseDict.TryGetValue(item, out Action action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning("No use case found for item: " + item.name);
        }
    }

    public void HealthUp()
    {
        health.ChangeHealth(health.maxHealth);
    }

    public void MaxHealthUp()
    {
        health.ChangeMaxHealth(1);
    }
}