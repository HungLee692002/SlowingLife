using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer itemContainer;
    public List<InventoryButton> buttonList;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < itemContainer.slots.Count && i < buttonList.Count; i++)
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
        for (int i = 0; i < itemContainer.slots.Count && i < buttonList.Count; i++)
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

    public virtual void OnClick(int id)
    {

    }
}
