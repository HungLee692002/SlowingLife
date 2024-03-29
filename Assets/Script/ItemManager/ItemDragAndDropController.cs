using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject itemIcon;
    RectTransform iconTransform;
    Image itemIconImage;
    Transform player;

    private void Awake()
    {
        player = GameManagement.instance.player.transform;
    }

    private void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 spawnPosition = player.position;
                    spawnPosition.x += UnityEngine.Random.Range(-3,3);
                    spawnPosition.y += UnityEngine.Random.Range(-3,3);
                    spawnPosition.z = -1;

                    Vector3 a =( UnityEngine.Random.insideUnitSphere.normalized * 3) + player.position;
                    float distance = Vector2.Distance(a, player.position);
                    Debug.Log(distance);


                    ItemSpawnManager.instance.SpawnItem(spawnPosition, itemSlot.item, itemSlot.count);

                    itemSlot.Clear();
                    itemIcon.SetActive(false);
                }
            }
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        //Check if is there any item is on the mouse
        if (this.itemSlot.item == null)
        {
            //set item to move to another slot
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;

            //swap the item on this slot and the item on the mouse
            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.Icon;
        }
    }
}
