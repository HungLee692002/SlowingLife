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
        for(int i = 0; i < itemContainer.slots.Count; i++)
        {
            buttonList[i].SetIndex(i);
        }

    }

    private void Show()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                buttonList[i].Clean();
            }else
            {
                buttonList[i].Set(itemContainer.slots[i]);
            }
        }
    }
}
