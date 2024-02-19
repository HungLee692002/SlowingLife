using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolBarController;

    int currentHightTool;

    private void Start()
    {
        Init();
        toolBarController.onChange += Highlight;
        Highlight(0);
    }
    public override void OnClick(int id)
    {
        toolBarController.Set(id);
        Highlight(id);
    }

    public void Highlight(int id)
    {
        buttonList[currentHightTool].HighLight(false);
        currentHightTool = id;
        buttonList[currentHightTool].HighLight(true);
    }
}
