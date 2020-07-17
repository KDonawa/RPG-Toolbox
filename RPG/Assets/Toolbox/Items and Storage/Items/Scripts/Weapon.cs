using UnityEngine;

public enum WeaponLocation
{
    Main_Hand,
    Off_Hand,
    Either,
    Both_Hands
}
//public enum WeaponType
//{
//    None,
//    Bow,
//    Sword,
//    Staff,
//    Shield
//}
//public enum WeaponSpaceOccupied
//{
//    One_Hand,
//    Both_Hands
//}

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : Equipment
{
    [SerializeField] WeaponLocation location = WeaponLocation.Main_Hand;
    //[SerializeField] WeaponType type = WeaponType.None;
    //[SerializeField] WeaponSpaceOccupied spaceOccupied = WeaponSpaceOccupied.One_Hand;

    public WeaponLocation Location => location;
    //public WeaponType Type => type;
    //public WeaponSpaceOccupied SpaceOccupied => spaceOccupied;


    public override string GetEquipmentTypeAsString()
    {
        //return type.ToString();
        return string.Empty;
    }

    public override string GetEquipmentLocationAsString()
    {
        return location.ToString().Replace('_', ' ');
    }

    protected override void SetItemType() => _itemType = ItemType.Weapon;
}
