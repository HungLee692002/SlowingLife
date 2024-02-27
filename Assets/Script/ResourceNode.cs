using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount ;
    [SerializeField] int dropCountMin ;
    [SerializeField] int dropCountMax ;
    [SerializeField] float spread = 0.75f;
    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] ResourceNodeType resourceType;


    public void Awake()
    {
        dropCount = Random.Range(dropCountMin, dropCountMax);
    }

    public override void Hit()
    {
        while(dropCount > 0)
        {
            dropCount--;

            Vector3 postion = transform.position;
            postion.x += spread * UnityEngine.Random.value - spread / 2;
            postion.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(postion, item,itemCountInOneDrop);
        }
        //Destroy when player hit
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(resourceType);
    }
}
