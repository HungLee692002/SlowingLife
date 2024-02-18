using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image Icon;
    [SerializeField] Text Text;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    //Set item into slot in inventory
    public void Set(ItemSlot slot)
    {
        //Set icon for item
        Icon.sprite = slot.item.Icon;
        Icon.gameObject.SetActive(true);
        //If item is stackable then display quantity
        if (slot.item.Stackable)
        {
            Text.gameObject.SetActive(true);
            Text.text = slot.count.ToString();
        }
        //if not then not display
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

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemContainer inventory = GameManagement.instance.itemContainer;

        GameManagement.instance.dragAndDropController.OnClick(inventory.slots[myIndex]);

        transform.parent.GetComponent<InventoryPanel>().Show();
    }
}
