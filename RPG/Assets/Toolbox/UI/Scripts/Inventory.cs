using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
/*
     TODO:
     -even if inventory is empty slots should populate the viewable area but with no name or item - can use an int to control the number
*/
public class Inventory : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Header("Testing")]
    [SerializeField] ItemContainer_SO startingItems = null;

    [Header("Values")]
    [SerializeField] float maxWeight = 200f;    

    [Header("Prefab / UI")]
    [SerializeField] InventorySlot slotPrefab = null;
    [SerializeField] GameObject slotContainer = null;
    //[SerializeField] Tooltip _tooltip = null;
    [SerializeField] TextMeshProUGUI filterOptionsText = null;
    #endregion

    #region MEMBER VARIABLES
    List<InventorySlot> _inventorySlots;
    //float _currentWeight;
    // int maxCapacity
    #endregion

    #region PROPERTIES
    public List<InventorySlot> InventorySlots => _inventorySlots;
    //public Tooltip Tooltip => _tooltip;
    //public EquipmentTooltip EquipmentTooltip => _equipmentTooltip;
    public float MaxWeight => maxWeight;
    #endregion

    #region INITIALIZATION
    public void Initialize()
    {
        _inventorySlots = new List<InventorySlot>();
        //_currentWeight = 0f;

        PopulateInventory(); // for testing

        //if (_tooltip != null) _tooltip.Hide();
        if (filterOptionsText != null) filterOptionsText.text = "All";
    }
    void PopulateInventory()
    {
        // in future will read from save data i think
        if (slotContainer == null) return;

        // remove any existing slots in inventory
        ItemSlot[] slots = slotContainer.GetComponentsInChildren<ItemSlot>();
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        _inventorySlots.Clear();

        // add slots based on starting items
        foreach (var itemSet in startingItems.itemSets)
        {
            for (int i = 0; i < itemSet.amount; i++)
            {
                AddItem(itemSet.item);
            }
        }
    }
    #endregion

    #region INVENTORY FILTERING
    public void FilterBasedOnNone() => FilterBasedOnType(ItemType.None);
    public void FilterBasedOnWeapons() => FilterBasedOnType(ItemType.Weapon);
    public void FilterBasedOnArmor() => FilterBasedOnType(ItemType.Armor);
    public void FilterBasedOnConsumables() => FilterBasedOnType(ItemType.Consumable);
    void FilterBasedOnType(ItemType itemType)
    {
        //print(itemType.ToString());
        foreach (var slot in _inventorySlots)
        {
            //TODO: be carful if slots are null
            if(itemType == ItemType.None) slot.gameObject.SetActive(true);
            else slot.gameObject.SetActive(slot.Item.ItemType == itemType);       
        }
        if (filterOptionsText == null) return;
        
        if (itemType == ItemType.None) filterOptionsText.text = "All";
        else filterOptionsText.text = itemType.ToString();
    }
    #endregion

    #region INVENTORY SORTING

    #endregion

    #region ADDING AND REMOVING ITEMS
    
    public void AddItem(Item item/*, int amount*/)
    {
        // TODO: make bool and check if adding item exceeds weight before adding, return false if impossible and true otherwise 
        
        if (item == null) return;
        if (item.IsStackable)
        {
            InventorySlot slot = _inventorySlots.FindLast(x => x.Item.ID == item.ID);
            if(slot != null && item.MaxStack >= slot.Quantity + 1)
            {
                slot.IncrementItemCount();
                return;
            }          
        }
        InventorySlot newSlot = Instantiate(slotPrefab, slotContainer.transform);
        _inventorySlots.Add(newSlot);
        newSlot.Initialize(item, this);

        //if (item.IsStackable)
        //{
        //    while (amount > 0)
        //    {
        //        InventorySlot slot = _inventorySlots.FindLast(x => x.Item.ID == item.ID);
        //        if (slot != null)
        //        {
        //            if (item.MaxStack >= slot.Quantity + amount)
        //            {
        //                slot.IncrementItemCount(amount);
        //                return;
        //            }
        //            else
        //            {
        //                int amountAbleToAdd = item.MaxStack - slot.Quantity;
        //                slot.IncrementItemCount(amountAbleToAdd);                        
        //                amount -= amountAbleToAdd;
        //            }
        //        }
        //        InventorySlot newSlot = Instantiate(slotPrefab, slotContainer.transform);
        //        _inventorySlots.Add(newSlot);
        //        newSlot.Initialize(item, this);             
        //    }            
        //}
        //else
        //{
        //    for (int i = 0; i < amount; i++)
        //    {
        //        InventorySlot newSlot = Instantiate(slotPrefab, slotContainer.transform);
        //        _inventorySlots.Add(newSlot);
        //        newSlot.Initialize(item,this);
        //        newSlot.IncrementItemCount(1);
        //    }
        //}               
    }
    public void RemoveItem(Item item)
    {
        _inventorySlots.FindLast(x => x.Item.ID == item.ID).DecrementItemCount();
    }
    public void RemoveSlot(InventorySlot slot)
    {
        _inventorySlots.Remove(slot);
    }
    #endregion

    #region INTERACTING WITH ITEMS IN INVENTORY
    public event Action<Equipment> OnEquipmentUsed;
    public event Action<Consumable> OnConsumableUsed;
    public event Action<Item> OnTooltipEnabled;
    public event Action OnTooltipDisabled;

    public void MouseEnterInventorySlot(Item item) => OnTooltipEnabled?.Invoke(item);
    public void MouseExitInventorySlot() => OnTooltipDisabled?.Invoke();
    public void RightClickItemSlot(Item item)
    {       
        OnTooltipDisabled?.Invoke();

        if (item is Equipment) OnEquipmentUsed?.Invoke(item as Equipment);
        else if(item is Consumable) OnConsumableUsed?.Invoke(item as Consumable);
    }
    #endregion

}

