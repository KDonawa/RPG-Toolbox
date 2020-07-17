using UnityEngine;
using System;

using System.Collections.Generic;
/*
     TODO:
     -
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
    
    public event Action<Item> OnTooltipEnabled;
    public event Action OnTooltipDisabled;

    public void MouseEnterEquipmentSlot(Item item) => OnTooltipEnabled?.Invoke(item);
    public void MouseExitEquipmentSlot() => OnTooltipDisabled?.Invoke();    
    public void RightClickEquipmentSlot() => OnTooltipDisabled?.Invoke();

    #endregion

    #region EQUIPPING AND UNEQUIPPING
    public event Action<Equipment> EquipmentUnequipped;

    public void Equip(Equipment equipment)
    {
        if (equipment == null) return;
        if(equipment is Weapon) EquipWeapon(equipment as Weapon);
        else if (equipment is Armor) EquipArmor(equipment as Armor);
    }
    public void Unequip(Equipment equipment)
    {
        //Debug.Log("Unequipping");
        EquipmentUnequipped?.Invoke(equipment);
    }
    void EquipWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        
        WeaponSlot mainHandSlot = FindWeaponSlotByType(WeaponSlotType.Main_Hand_Weapon);
        if (mainHandSlot == null)
        {
            Debug.LogWarning("Panel does not have a main hand weapon slot");
            return;
        }
        WeaponSlot offHandSlot = FindWeaponSlotByType(WeaponSlotType.Off_Hand_Weapon);
        if (offHandSlot == null)
        {
            Debug.LogWarning("Panel does not have an off hand weapon slot");
            return;
        }

        switch (weapon.Location)
        {
            case WeaponLocation.Main_Hand:
                mainHandSlot.ReceiveEquipment(weapon);
                break;
            case WeaponLocation.Off_Hand:
                if(!mainHandSlot.IsEmpty && (mainHandSlot.Item as Weapon).Location == WeaponLocation.Both_Hands)
                    mainHandSlot.RemoveEquipment();
                offHandSlot.ReceiveEquipment(weapon);
                break;
            case WeaponLocation.Either:
                if (mainHandSlot.IsEmpty) mainHandSlot.ReceiveEquipment(weapon);
                else if((mainHandSlot.Item as Weapon).Location == WeaponLocation.Both_Hands) 
                    mainHandSlot.ReceiveEquipment(weapon);
                else if (offHandSlot.IsEmpty) offHandSlot.ReceiveEquipment(weapon);
                else mainHandSlot.ReceiveEquipment(weapon);
                break;
            case WeaponLocation.Both_Hands:
                offHandSlot.RemoveEquipment();
                mainHandSlot.ReceiveEquipment(weapon);
                break;
            default:
                break;
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
    #endregion

    #region UTILITY
    public ArmorSlot FindArmorSlot(Armor armor)
    {
        return FindArmorSlotByType(armor.ArmorType);
    }
    public WeaponSlot FindWeaponSlot(Weapon weapon)
    {
        return weapon.Location != WeaponLocation.Off_Hand ?
                FindWeaponSlotByType(WeaponSlotType.Main_Hand_Weapon) :
                FindWeaponSlotByType(WeaponSlotType.Off_Hand_Weapon);
    }
    ArmorSlot FindArmorSlotByType(ArmorType slotType) => Array.Find(armorSlots, x => x.ArmorSlotType == slotType);
    WeaponSlot FindWeaponSlotByType(WeaponSlotType slotType) => Array.Find(weaponSlots, x => x.WeaponSlotType == slotType);
    #endregion
}
