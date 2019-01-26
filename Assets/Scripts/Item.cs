using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Permanent,
        NonPermanent
    }
    public string Name;
    public ItemType itemType;
    public Sprite sprite;
    public AudioClip audioClip;

    public Item(string name, ItemType type)
    {
        Name = name;
        itemType = type;
    }

    public void Interact()
    {
        if (itemType == ItemType.Permanent)
        {
            InventoryUI.Instance.AddNumberItem(this);
            Destroy(gameObject);
        }

        else
        {
            //do the other thing
            
        }
    }

    public void ItemUsed()
    {

    }
}
