using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer itemContainer;
    [SerializeField] List<InventoryButton> buttonList;

    private void Start()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            //assign index for each slot in inventory
            buttonList[i].SetIndex(i);
        }
    }

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            //if slot is null then not display anything
            if (itemContainer.slots[i].item == null)
            {
                buttonList[i].Clean();
            }
            //if not display item in that slot
            else
            {
                buttonList[i].Set(itemContainer.slots[i]);
            }
        }
    }
}
