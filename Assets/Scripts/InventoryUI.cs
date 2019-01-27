using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private const int inventorySlots = 3;
    public Image[] numberImage = new Image[inventorySlots];
    public Image itemImage;
    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    
    public void AddNumberItem(Item item)
    {
        if (PlayerInventory.numberInventory.Count < 3)
        {
            PlayerInventory.numberInventory.Add(item);
            numberImage[PlayerInventory.numberInventory.Count - 1].sprite = item.sprite;
            numberImage[PlayerInventory.numberInventory.Count - 1].enabled = true;
        }
    }

    //public void AddUseableItem(Item item)
    //{
    //    if (PlayerInventory.itemInventory.Count < 1)
    //    {
    //        PlayerInventory.itemInventory.Add(item);
    //        numberImage[PlayerInventory.itemInventory.Count - 1].sprite = item.sprite;
    //        numberImage[PlayerInventory.itemInventory.Count - 1].enabled = true;
    //    }
    //}

    //public void RemoveItem(Item item)
    //{
    //    if (PlayerInventory.itemInventory.Count > 0)
    //    {
    //        numberImage[PlayerInventory.itemInventory.Count - 1].sprite = null;
    //        numberImage[PlayerInventory.itemInventory.Count - 1].enabled = false;
    //        PlayerInventory.itemInventory.Remove(item);
    //    }
    //}
}
