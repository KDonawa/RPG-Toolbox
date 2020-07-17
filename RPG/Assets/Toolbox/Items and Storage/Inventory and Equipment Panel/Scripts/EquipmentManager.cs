using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     TODO:
     -We need to check if gear can be equipped
*/
public class EquipmentManager : MonoBehaviour
{
    [SerializeField] Inventory inventory = null;
    [SerializeField] EquipmentPanel equipmentPanel = null;

    private void Awake()
    {
        if(inventory != null) inventory.Initialize();
        if (equipmentPanel != null) equipmentPanel.Initialize();
    }
    void Start()
    {
        if (inventory != null) inventory.EquipmentUsed += OnEquipmentUsed;
        if (equipmentPanel != null) equipmentPanel.EquipmentUnequipped += OnEquipmentUnequipped;
    }
    private void OnDestroy()
    {
        if (inventory != null) inventory.EquipmentUsed -= OnEquipmentUsed;
        if (equipmentPanel != null) equipmentPanel.EquipmentUnequipped -= OnEquipmentUnequipped;
    }

    void OnEquipmentUsed(Equipment equipment)
    {
        if (inventory != null && equipmentPanel != null)
        {
            // we need to check if we have the space to unequip first then do below
            inventory.RemoveItem(equipment);
            equipmentPanel.Equip(equipment);
        }

        // do other stuff like spawn prefab, etc.
    }
    void OnEquipmentUnequipped(Equipment equipment)
    {
        if (inventory != null) inventory.AddItem(equipment);
        // do other stuff
    }

}
