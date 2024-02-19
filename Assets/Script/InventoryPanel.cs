using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManagement.instance.dragAndDropController.OnClick(itemContainer.slots[id]);
        Show();
    }
}
