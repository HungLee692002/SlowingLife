using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Text Text;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        Icon.sprite = slot.item.Icon;

        if (slot.item.Stackable)
        {
            Text.gameObject.SetActive(true);
            Text.text = slot.count.ToString();
        }
        else
        {
            Text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
    }
}
