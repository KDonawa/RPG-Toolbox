using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item Container")]
public class ItemContainer_SO : ScriptableObject
{
    public ItemSet[] itemSets = null;
    
}
[System.Serializable]
public struct ItemSet
{
    public Item item;
    public int amount;
}
