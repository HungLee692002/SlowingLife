using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolBarSize = 10;

    int selectedTool;

    public Action<int> onChange;

    internal void Set(int id)
    {
        selectedTool = id;
    }

    public Item GetItem
    {
        get
        {
            return GameManagement.instance.itemContainer.slots[selectedTool].item;
        }
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y * -1;

        if(delta != 0)
        {
            if(delta > 0)
            {
                selectedTool += 1;

                selectedTool = (selectedTool >= toolBarSize ? 0 : selectedTool);
            } else
            {
                selectedTool -= 1;

                selectedTool = (selectedTool < 0 ? toolBarSize - 1 : selectedTool);

            }
            //Debug.Log(selectedTool);
            onChange?.Invoke(selectedTool);
        }
    }
}
