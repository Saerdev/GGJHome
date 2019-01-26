using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IIinteractiable
{
    public enum ItemType
    {
        Pickup,
        NonPickup
    }
    public string Name;
    public ItemType itemType;

    public void Interact()
    {
        if (itemType== ItemType.Pickup)
        {
            PlayerInventory.Inventory.Add(this);
            Destroy(gameObject);
        }
        else
        {
            //play audio or whatever 
        }
    }
}
