using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlot
{
    [SerializeField] protected TextMeshProUGUI itemNameGUI = null;

    Inventory _inventory = null;
    protected int _quantity;
    public int Quantity => _quantity;

    #region INITIALIZATION
    public void Initialize(Item item, Inventory inventory)
    {
        _item = item;
        _inventory = inventory;
        _quantity = 1;

        if (itemNameGUI != null)
        {
            itemNameGUI.text = item.Name;
            itemNameGUI.gameObject.SetActive(true);
        }

    }
    #endregion

    #region SLOT INTERACTIONS
    protected override void OnPointerRightClick()
    {
        if (_item == null) return;
        _inventory.RightClickItemSlot(Item);
    }
    public override void OnPointerEnter(PointerEventData eventData) => _inventory.MouseEnterInventorySlot(Item);
    public override void OnPointerExit(PointerEventData eventData) => _inventory.MouseExitInventorySlot();
    #endregion

    #region UTILITY

    public void DecrementItemCount() => UpdateItemCount(-1);
    public void IncrementItemCount() => UpdateItemCount(1);
    void UpdateItemCount(int amount)
    {
        _quantity += amount;
        if (_quantity <= 0)
        {
            _inventory.RemoveSlot(this);
            Destroy(gameObject);
        }

        if (itemNameGUI != null) itemNameGUI.text = $"{_item.Name} ({_quantity})";
    }

    #endregion
}
