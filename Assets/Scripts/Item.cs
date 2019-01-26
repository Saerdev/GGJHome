using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Pickup,
        NonPickup
    }
    public string Name;
    public ItemType itemType;
    public Sprite sprite;

    public Item(string name, ItemType type)
    {
        Name = name;
        itemType = type;
    }

    public void Interact()
    {
        if (itemType== ItemType.Pickup)
        {
            InventoryUI.Instance.AddItem(this);
            Destroy(gameObject);
        }
        else
        {
            //play audio or whatever 
        }
    }
}
