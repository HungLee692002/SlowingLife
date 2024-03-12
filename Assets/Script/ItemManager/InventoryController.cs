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
            if (inventory.activeInHierarchy)
            {
                Close();
                topPanel.SetActive(false);
                toolbarPanel.SetActive(true);
            } else
            {
                Open();
                topPanel.SetActive(true);
                toolbarPanel.SetActive(false);
            }
        }
    }

    public void Open()
    {
        inventory.SetActive(true);
    }

    public void Close()
    {
        inventory.SetActive(false);
    }
}
