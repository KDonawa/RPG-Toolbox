using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// do i need this class
// how will i deal with belts/jewelry/capes


public abstract class Equipment : Item
{
    public abstract string GetEquipmentTypeAsString();
    public abstract string GetEquipmentLocationAsString();
}
