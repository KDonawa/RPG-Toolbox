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
        if (inventory != null) inventory.OnEquipmentUsed += Equip;
        if (equipmentPanel != null) equipmentPanel.OnEquipmentUnequipped += SendToInventory;
    }
    private void OnDestroy()
    {
        if (inventory != null) inventory.OnEquipmentUsed -= Equip;
        if (equipmentPanel != null) equipmentPanel.OnEquipmentUnequipped -= SendToInventory;
    }

    void Equip(Equipment equipment)
    {
        if (inventory != null) inventory.RemoveItem(equipment);
        if (equipmentPanel != null) equipmentPanel.Equip(equipment);
        // do other stuff like spawn prefab, etc.
    }
    void SendToInventory(Equipment equipment)
    {
        if (inventory != null) inventory.AddItem(equipment);
        // do other stuff
    }

}
