using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlot : EquipmentSlot
{
    [SerializeField] ArmorType armortSlotType = ArmorType.None;

    public ArmorType ArmortSlotType => armortSlotType;
}
