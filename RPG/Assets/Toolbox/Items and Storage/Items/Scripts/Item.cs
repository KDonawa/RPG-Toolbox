using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
     TODO:
     -make jewelry item which derives from equipment class
*/
public enum ItemType
{
    All,
    Weapon,
    Armor,
    Potion,
    Food,
    Quest
}
public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Unique
}

public abstract class Item : ScriptableObject
{
    [Header("Tooltip Info")]
    [SerializeField] string itemName = "Item Name";
    [SerializeField] protected ItemRarity itemRarity = ItemRarity.Common;
    //[SerializeField] string description = "Description goes here";
    //[SerializeField] float sellValue = 0f;
    //[SerializeField] float weight = 0f;

    [Header("Unique ID")]
    [SerializeField] int id = -1;

    [Header("Visual Components")]
    //[SerializeField] Sprite sprite = null;
    //[SerializeField] Material material = null;

    [Header("Properties")]
    [SerializeField] bool isStackable = false;
    [SerializeField] int maxStack = 99;
    //[SerializeField] bool isIndestructable = false;
    //[SerializeField] bool isInteractable = false;
    //[SerializeField] bool isStorable = false;
    //[SerializeField] bool isUnique = false;
    //[SerializeField] bool isQuestItem = false;  
    //[SerializeField] bool isDestroyedOnUse = false;

    protected ItemType _itemType = ItemType.All;


    #region Properties
    public string Name => itemName;
    public ItemType ItemType => _itemType;
    public ItemRarity ItemRarity => itemRarity;
    //public string Description => description;
    public int ID => id;
    public bool IsStackable => isStackable;
    public int MaxStack => maxStack;
    #endregion

    private void Awake()
    {
        SetItemType();
    }
    protected abstract void SetItemType();

}


