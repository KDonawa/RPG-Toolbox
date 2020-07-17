using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable/Health Potion")]
public class HealthPotion : Consumable
{
    protected override void SetItemType() => _itemType = ItemType.Potion;
}
