using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject toolbarPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
            toolbarPanel.SetActive(!inventory.activeInHierarchy);
        }
    }
}
