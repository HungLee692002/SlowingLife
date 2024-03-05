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

    public void Set(Item item, int count)
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

    public void Remove(Item itemToRemove, int count = 1)
    {
        if (itemToRemove.Stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if (itemSlot == null) { return; }

            itemSlot.count -= count;
            if(itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        } else
        {
            while(count > 0)
            {
                count -=1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null) { break; }

                itemSlot.Clear();

            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for(int i = 0;i < slots.Count;i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }

        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot item = slots.Find(x => x.item == checkingItem.item);

        if(item == null) { return false; }

        if(checkingItem.item.Stackable)
        {
            return item.count > checkingItem.count;
        }
        return true;
    }
}
