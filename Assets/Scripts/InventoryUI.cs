using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] itemImage = new Image[3];
    public Item item;
    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItem(item);
        }
    }

    public void AddItem(Item item)
    {
        PlayerInventory.Inventory.Add(item);

        PlayerInventory.Inventory[PlayerInventory.Inventory.Count - 1] = item;
        itemImage[PlayerInventory.Inventory.Count - 1].sprite = item.sprite;
        itemImage[PlayerInventory.Inventory.Count - 1].enabled = true;
    }
}
