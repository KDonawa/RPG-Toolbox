using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
     TODO:
     -I don't want to have to make a new tooltip prefab for every tooltip that i want to show
     -want to display equipment grade and type: rare bow
     -display equipment location on same line: [rare sword        One Hand]
*/
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] ItemRarityColors_SO itemRarityColors = null;
    [SerializeField] TextMeshProUGUI equippedStatus = null;
    [SerializeField] TextMeshProUGUI itemName = null;
    [SerializeField] TextMeshProUGUI itemType = null;
    [SerializeField] TextMeshProUGUI itemDescription = null;
    [SerializeField] TextMeshProUGUI itemRarity = null;
    [SerializeField] TextMeshProUGUI equipmentType = null;

    public ItemRarityColors_SO ItemRarityColors => itemRarityColors;
    public virtual void ShowTooltip(Item item, bool isEquipped = false)
    {
        if (item == null) return;
        
        equippedStatus.gameObject.SetActive(isEquipped);

        itemName.text = item.name;
        SetItemNameColor(itemName, item.ItemRarity);

        itemType.text = item.ItemType.ToString();

        itemDescription.text = item.Description;

        itemRarity.text = item.ItemRarity.ToString();

        gameObject.SetActive(true);

        //if (item is Equipment)
        //{

        //    // make funcs in equip class to return location and type
        //    //Weapon weapon = equipment as Weapon;
        //    //if(weapon != null)
        //    //{
        //    //    equipmentType.text = weapon.WeaponType;
        //    //}
        //    //else
        //    //{
        //    //    Armor armor = equipment as Armor;
        //    //    equipmentType.text = armor.armorLocation.ToString();
        //    //}
        //    //equipmentType.gameObject.SetActive(true);

        //    //Debug.Log(equipment.GetEquipmentLocationAsString());
        //    //Debug.Log(equipment.GetEquipmentTypeAsString());
        //}
        //else
        //{
        //    //equipmentType.gameObject.SetActive(false);
        //}
    }
    public virtual void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void SetItemNameColor(TextMeshProUGUI itemName, ItemRarity itemRarity)
    {
        itemName.color = itemRarityColors.GetItemRarityColor(itemRarity);
    }
}
