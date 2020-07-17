using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponSlotType
{
    None,
    Main_Hand_Weapon,
    Off_Hand_Weapon,
}
public class WeaponSlot : EquipmentSlot
{
    [SerializeField] WeaponSlotType weaponSlotType = WeaponSlotType.None;

    public WeaponSlotType WeaponSlotType => weaponSlotType;
}
