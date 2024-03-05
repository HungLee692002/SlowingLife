using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject topPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
            topPanel.SetActive(!topPanel.activeInHierarchy);
            toolbarPanel.SetActive(!inventory.activeInHierarchy);
        }
    }
}
