using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChestInteract : Interactable, IPersistant
{
    [SerializeField] bool opened;
    [SerializeField] AudioClip openChestAudioClip;
    [SerializeField] AudioClip closeChestAudioClip;
    [SerializeField] ItemContainer itemContainer;

    private void Start()
    {
        if (itemContainer == null)
        {
            Init();
        }

    }
    private void Init()
    {

        itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
        itemContainer.Init();
    }

    public override void Interact(Character character)
    {
        if (opened == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
    }

    public void Open(Character character)
    {
        opened = true;

        AudioManager.instance.Play(openChestAudioClip);

        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        opened = false;

        AudioManager.instance.Play(closeChestAudioClip);

        character.GetComponent<ItemContainerInteractController>().Close();
    }

    [Serializable]
    public class SaveItemData
    {
        public int itemId;

        public int count;

        public SaveItemData(int itemId, int count)
        {
            this.itemId = itemId;
            this.count = count;
        }
    }

    [Serializable]
    public class ToSave
    {
        public List<SaveItemData> data;

        public ToSave()
        {
            data = new List<SaveItemData>();
        }
    }

    public string Read()
    {
        ToSave toSave = new ToSave();

        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.data.Add(new SaveItemData(-1, 0));
            }
            else
            {
                SaveItemData item = new SaveItemData(itemContainer.slots[i].item.id, itemContainer.slots[i].count);
                toSave.data.Add(item);
            }
        }
        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}") { return; }
        if (itemContainer == null)
        {
            Init();
        }

        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);
        for (int i = 0; i < toLoad.data.Count; i++)
        {
            if (toLoad.data[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GameManagement.instance.itemDB.items.FirstOrDefault(x => x.id == toLoad.data[i].itemId);
                itemContainer.slots[i].count = toLoad.data[i].count;
            }
        }
    }
}
