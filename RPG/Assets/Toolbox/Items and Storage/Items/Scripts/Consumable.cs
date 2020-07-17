using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    Health,
    Mana
}

[CreateAssetMenu(menuName = "Item/Consumable")]
public class Consumable : Item
{
    protected override void SetItemType() => _itemType = ItemType.Consumable;
}
