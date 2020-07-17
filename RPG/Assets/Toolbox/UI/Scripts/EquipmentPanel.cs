using UnityEngine;
using System;

using System.Collections.Generic;
/*
     TODO:
     -put weapon in correct slot and unequip necesarry weapons based on handedness of weapon being equipped
*/
public class EquipmentPanel : MonoBehaviour
{
    [Header("Equipements Slots")]
    [SerializeField] WeaponSlot[] weaponSlots = null;
    [SerializeField] ArmorSlot[] armorSlots = null;    

    [Header("Slot Colors")]
    public Color vacantSlotColor = new Color();
    public Color occupiedSlotColor = new Color();    
    

    #region INITIALIZATION
    public void Initialize()
    {
        foreach (var slot in armorSlots)
        {
            slot.Initialize(this);
        }
        foreach (var slot in weaponSlots)
        {
            slot.Initialize(this);
        }
    }
    #endregion

    #region INETERACTING WITH EQUIPMENT IN SLOTS
    public event Action<Equipment> OnEquipmentUnequipped;
    public event Action<Item> OnTooltipEnabled;
    public event Action OnTooltipDisabled;

    public void MouseEnterEquipmentSlot(Item item) => OnTooltipEnabled?.Invoke(item);
    public void MouseExitEquipmentSlot() => OnTooltipDisabled?.Invoke();
    public void RightClickEquipmentSlot() => OnTooltipDisabled?.Invoke();
    #endregion

    public void Equip(Equipment equipment)
    {
        if (equipment == null) return;
        if(equipment is Weapon) EquipWeapon(equipment as Weapon);
        else if (equipment is Armor) EquipArmor(equipment as Armor);

    }
    public void Unequip(Equipment equipment)
    {
        //Debug.Log("Unequipping");
        OnEquipmentUnequipped?.Invoke(equipment);
    }
    void EquipWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        //Debug.Log("equipping weapon");
        /*
         Cases:
         -equipping 1h and main and off are full or both empty => unequip current main hand and put new
         -equipping 1h to off => check if 2h equipped and remove, if not, unequip current off and put new in off
         -equipping 2h => unequip main and off
         -equipping off => check if 2h equipped and remove, if not, unequip current off and put new in off
         -equipping 1h but main is full and off is not, equip to off
         */
         /*
         Rules:
         When will we equip to off?: when either weapon is off only or weapon is 1h and main is full and off empty
         This means that in every other case, we will equip to main
         */

        // For now, make it easy and just assume that all weapons are 1h and go into main hand slot
        WeaponSlot mainHandSlot = FindWeaponSlotByType(WeaponSlotType.Main_Hand_Weapon);
        if (mainHandSlot != null)
        {
            if(!mainHandSlot.IsEmpty) mainHandSlot.RemoveEquipment();
            mainHandSlot.ReceiveEquipment(weapon);
        }        

    }
    void EquipArmor(Armor armor)
    {
        if (armor == null) return;
        //Debug.Log("equipping armor");
        
        ArmorSlot armorSlot = FindArmorSlot(armor);
        if (armorSlot != null)
        {
            if (!armorSlot.IsEmpty) armorSlot.RemoveEquipment();
            armorSlot.ReceiveEquipment(armor);
        }
    }
    

    #region UTILITY
    public ArmorSlot FindArmorSlot(Armor armor)
    {
        return Array.Find(armorSlots, x => x.ArmortSlotType == armor.ArmorType);
    }
    public WeaponSlot FindWeaponSlot(Weapon weapon)
    {
        return weapon.Location != WeaponLocation.Off_Hand ?
                FindWeaponSlotByType(WeaponSlotType.Main_Hand_Weapon) :
                FindWeaponSlotByType(WeaponSlotType.Off_Hand_Weapon);
    }
    WeaponSlot FindWeaponSlotByType(WeaponSlotType slotType) => Array.Find(weaponSlots, x => x.WeaponSlotType == slotType);
    #endregion
}
