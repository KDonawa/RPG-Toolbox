using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

/*
     TODO:
     -left click will open up some options for the item
*/
public abstract class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{   

    protected Item _item;

    public Item Item => _item;
    public bool IsEmpty => _item == null;

    

    #region INTERFACES
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Debug.Log("left click");
                break;
            case PointerEventData.InputButton.Right:
                OnPointerRightClick();
                break;
            case PointerEventData.InputButton.Middle:
                Debug.Log("middle click");
                break;
        }
    }
    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    #endregion

    #region SLOT INTERACTIONS

    protected abstract void OnPointerRightClick();  

    #endregion
    
}
