using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     TODO:
     -
*/
public class TooltipManager : MonoBehaviour
{
    [SerializeField] ItemTooltip itemTooltipInventory = null;
    [SerializeField] ItemTooltip equippedItemTooltipInventory = null;
    [SerializeField] ItemTooltip equipmentPanelTooltip = null;
    [SerializeField] Inventory inventory = null;
    [SerializeField] EquipmentPanel equipmentPanel = null;

    #region INITIALIZATION
    private void Awake()
    {
        HideInventoryTooltips();        
    }

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.TooltipEnabled += ShowInventoryTooltips;
            inventory.TooltipDisabled += HideInventoryTooltips;
        }
        if (equipmentPanel != null)
        {
            equipmentPanel.OnTooltipEnabled += ShowEquipmentPanelTooltip;
            equipmentPanel.OnTooltipDisabled += HideEquipmentPanelTooltip;
        }
    }
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.TooltipEnabled -= ShowInventoryTooltips;
            inventory.TooltipDisabled -= HideInventoryTooltips;
        }
        if (equipmentPanel != null)
        {
            equipmentPanel.OnTooltipEnabled -= ShowEquipmentPanelTooltip;
            equipmentPanel.OnTooltipDisabled -= HideEquipmentPanelTooltip;
        }
    }
    #endregion

    #region INVENTORY TOOLTIPS
    void HideInventoryTooltips()
    {
        if (itemTooltipInventory != null) itemTooltipInventory.HideTooltip();
        if (equippedItemTooltipInventory != null) equippedItemTooltipInventory.HideTooltip();
    }
    void ShowInventoryTooltips(Item item)
    {
        if (itemTooltipInventory == null) return;
        itemTooltipInventory.ShowTooltip(item);

        if (equipmentPanel == null || equippedItemTooltipInventory == null) return;
        ShowTooltipForItemEquipped(item);
    }
    void ShowTooltipForItemEquipped(Item item)
    {
        Item equippedItem = null;
        if (item is Weapon)
        {
            equippedItem = equipmentPanel.FindWeaponSlot(item as Weapon).Item;

            //if (equippedItem == null) Debug.Log("no weapon equipped");
        }
        else if (item is Armor)
        {
            equippedItem = equipmentPanel.FindArmorSlot(item as Armor).Item;

            //if (equippedItem == null) Debug.Log("no armor equipped");
        }

        if (equippedItem == null) return;

        equippedItemTooltipInventory.ShowTooltip(equippedItem, true);
    }
    #endregion

    #region EQUIPMENT PANEL TOOLTIP
    void ShowEquipmentPanelTooltip(Item item)
    {
        if (equipmentPanelTooltip != null) equipmentPanelTooltip.ShowTooltip(item, true);
    }
    void HideEquipmentPanelTooltip()
    {
        if (equipmentPanelTooltip != null) equipmentPanelTooltip.HideTooltip();
    }
    #endregion

    #region SKILLBAR TOOLTIP

    #endregion

}
