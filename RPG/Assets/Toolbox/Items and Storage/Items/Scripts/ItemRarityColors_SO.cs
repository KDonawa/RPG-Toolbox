using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemRarityColors_SO : ScriptableObject
{
    [SerializeField] Color commonItemColor = new Color();
    [SerializeField] Color uncommonItemColor = new Color();
    [SerializeField] Color rareItemColor = new Color();
    [SerializeField] Color epicItemColor = new Color();
    [SerializeField] Color legendaryItemColor = new Color();
    [SerializeField] Color uniqueItemColor = new Color();

    public Color GetItemRarityColor(ItemRarity itemRarity)
    {
        
        switch (itemRarity)
        {
            case ItemRarity.Common:
                return commonItemColor;
            case ItemRarity.Uncommon:
                return uncommonItemColor;
            case ItemRarity.Rare:
                return rareItemColor;
            case ItemRarity.Epic:
                return epicItemColor;
            case ItemRarity.Legendary:
                return legendaryItemColor;
            case ItemRarity.Unique:
                return uniqueItemColor;
            default:
                return commonItemColor;
        }
    }
}
