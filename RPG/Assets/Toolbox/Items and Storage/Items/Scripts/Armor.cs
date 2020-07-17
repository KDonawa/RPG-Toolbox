using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    None,
    Head,
    Chest,
    Hand,
    Legs,
    Feet,
    Back,
    Belt
}
//public enum ArmorWeight
//{
//    None,
//    Light,
//    Medium,
//    Heavy
//}

[CreateAssetMenu(menuName = "Item/Armor")]
public class Armor : Equipment
{
    [SerializeField] ArmorType armorType = ArmorType.Chest;
    //[SerializeField] ArmorWeight armorWeight = ArmorWeight.None;

    public ArmorType ArmorType => armorType;
    //public ArmorWeight ArmorWeight => armorWeight;

    public override string GetEquipmentTypeAsString()
    {
        //return string.Concat(armorType.ToString(), " Armor");
        return string.Empty;
    }

    public override string GetEquipmentLocationAsString()
    {
        return armorType.ToString();
    }

    protected override void SetItemType() =>_itemType = ItemType.Armor;
}
