using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;

    InventoryController inventoryController;

    [SerializeField] ItemContainerPanel containerPanel;

    Transform targetChest;

    [SerializeField] float maxDistance = 4f;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        if(targetChest != null)
        {
            float distance = Vector2.Distance(targetChest.position, transform.position);

            if(distance > maxDistance)
            {
                targetChest.GetComponent<ChestInteract>().Close(GetComponent<Character>());
            }
        }
    }

    public void Open(ItemContainer itemContainer, Transform openedChest)
    {
        targetItemContainer = itemContainer;
        containerPanel.itemContainer = targetItemContainer;
        containerPanel.gameObject.SetActive(true);
        inventoryController.Open();
        targetChest = openedChest;
    }

    public void Close()
    {
        containerPanel.gameObject.SetActive(false);
        inventoryController.Close();
        targetChest = null;
    }

}
