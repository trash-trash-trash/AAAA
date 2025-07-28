using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
[Serializable]
public class ItemSO : ScriptableObject
{
    public event Action AnnounceReset;
    
    public string itemName;
    public Sprite icon;
    public GameObject prefab;

    public void Reset()
    {
        AnnounceReset?.Invoke();
    }
}