using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManagement.instance.dragAndDropController.OnClick(itemContainer.slots[id]);
        Show();
    }
}
