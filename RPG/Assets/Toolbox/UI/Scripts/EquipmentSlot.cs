using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public abstract class EquipmentSlot : ItemSlot
{
    [SerializeField] protected TextMeshProUGUI slotText = null;

    #region MEMBER VARIABLES
    protected EquipmentPanel _equipmentPanel = null;
    #endregion


    #region INITIALIZATION
    public void Initialize(EquipmentPanel equipmentPanel)
    {
        _equipmentPanel = equipmentPanel;

        slotText.color = equipmentPanel.vacantSlotColor;
    }
    #endregion

    #region SLOT INTERACTIONS
    protected override void OnPointerRightClick()
    {
        if (_item == null) return;
        RemoveEquipment();
        _equipmentPanel.RightClickEquipmentSlot();
    }
    public override void OnPointerEnter(PointerEventData eventData) => _equipmentPanel.MouseEnterEquipmentSlot(Item);

    public override void OnPointerExit(PointerEventData eventData) => _equipmentPanel.MouseExitEquipmentSlot();
    #endregion

    #region UTILITY
    public void ReceiveEquipment(Equipment equipment)
    {
        //Debug.Log("receiving equipment");
        _item = equipment;
        slotText.color = _equipmentPanel.occupiedSlotColor;
    }
    public void RemoveEquipment()
    {
        //Debug.Log("removing equipment");
        _equipmentPanel.Unequip(_item as Equipment);
        _item = null;
        slotText.color = _equipmentPanel.vacantSlotColor;
    }
    #endregion
}
