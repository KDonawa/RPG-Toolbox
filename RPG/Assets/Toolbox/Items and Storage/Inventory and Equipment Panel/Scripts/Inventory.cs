using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
/*
     TODO:
     -
*/
public class Inventory : MonoBehaviour
{
    #region INSPECTOR VARIABLES
    [Header("Testing")]
    [SerializeField] ItemContainer_SO startingItems = null;

    [Header("Utility")]
    [SerializeField] TabGroup filterOptions = null;

    //[Header("Values")]
    //[SerializeField] float maxWeight = 200f;    

    [Header("Prefab / UI")]
    [SerializeField] InventorySlot slotPrefab = null;
    [SerializeField] GameObject slotContainer = null;
    //[SerializeField] Tooltip _tooltip = null;
    //[SerializeField] TextMeshProUGUI filterOptionsText = null;
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
    //public float MaxWeight => maxWeight;
    #endregion

    #region INITIALIZATION
    public void Initialize()
    {
        _inventorySlots = new List<InventorySlot>();
        //_currentWeight = 0f;

        InitializeFilterOptions();
        PopulateInventory(); // for testing

    }
    void InitializeFilterOptions()
    {
        if (filterOptions == null) return;

        filterOptions.InitilalizeTabGroup(Enum.GetNames(typeof(ItemType)), FilterBasedOnType);
        //filterOptions.AddTabButton("All");
        
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
    void FilterBasedOnType(string name)
    {
        //print(itemType.ToString());
        foreach (var slot in _inventorySlots)
        {         
            if(name == "All") slot.gameObject.SetActive(true);
            else slot.gameObject.SetActive(slot.Item.ItemType.ToString() == name);       
        }
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
        //InventorySlot newSlot = new InventorySlot(item);
        InventorySlot newSlot = Instantiate(slotPrefab, slotContainer.transform);
        _inventorySlots.Add(newSlot);
        newSlot.Initialize(item, this);
            
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
    public event Action<Equipment> EquipmentUsed;
    public event Action<Consumable> ConsumableUsed;
    public event Action<Item> TooltipEnabled;
    public event Action TooltipDisabled;

    public void MouseEnterInventorySlot(Item item) => TooltipEnabled?.Invoke(item);
    public void MouseExitInventorySlot() => TooltipDisabled?.Invoke();
    public void RightClickItemSlot(Item item)
    {       
        TooltipDisabled?.Invoke();

        if (item is Equipment) EquipmentUsed?.Invoke(item as Equipment);
        else if(item is Consumable) ConsumableUsed?.Invoke(item as Consumable);
    }
    #endregion

}

