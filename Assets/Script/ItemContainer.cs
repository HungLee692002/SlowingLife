using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }

    public void Set(Item item,int count)
    {
        this.item = item;
        this.count = count;
        
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Item item, int count = 1)
    {
        //If item is stackable
        if (item.Stackable)
        {

            ItemSlot itemSlot = slots.Find(x => x.item == item);
            //check if there is any item in slot
            if (itemSlot != null)
            {
                //Add that item to inventory slot
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        //If item is not stackable
        else
        {
            //find slot in inventory that not contain an item
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                //Add that item to inventory slot
                itemSlot.item = item;
            }
        }
    }
}
