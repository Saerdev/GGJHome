using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        InventoryItem,
        WorldItem
    }
    public string Name;
    public ItemType itemType;
    public Sprite sprite;
    public AudioClip audioClip;

    PlayerMovement playerMovement;
    MouseLook mouseLook;
    bool isInteracting;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        mouseLook = Camera.main.GetComponent<MouseLook>();
    }

    void Update()
    {
        if (isInteracting)
        {
            transform.position = Vector3.Lerp(transform.position, mouseLook.transform.position + mouseLook.transform.forward * 1, 20 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mouseLook.transform.rotation, 20 * Time.deltaTime);
        }
    }

    public Item(string name, ItemType type)
    {
        Name = name;
        itemType = type;
    }

    public virtual void Interact()
    {
        if (itemType == ItemType.InventoryItem)
        {
            if (!isInteracting)
            {
                playerMovement.enabled = false;
                InventoryUI.Instance.AddNumberItem(this);
                Cursor.lockState = CursorLockMode.None;
                isInteracting = true;

                if (audioClip != null)
                    AudioManager.Instance.Play(audioClip);
            }
            else
            {
                playerMovement.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                isInteracting = false;
                playerMovement.enabled = true;

                if (Name == "Paper")
                {
                    GameManager.Instance.ActivateDarkness();
                }

                Destroy(gameObject);
            }
        }

        else
        {
            //do the other thing
        }
    }
}
