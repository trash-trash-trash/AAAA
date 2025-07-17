using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
[Serializable]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
}