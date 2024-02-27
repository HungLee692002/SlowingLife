using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;

    //speed of item move to player
    [SerializeField] float speed = 0.01f;

    //range of pickup item
    [SerializeField] float pickUpDistance = 1.5f;

    //Time to live of item
    [SerializeField] float ttl = 10f;

    [SerializeField] Item item;

    [SerializeField] int count = 1;

    private void Awake()
    {
        player = GameManagement.instance.player.transform;
    }

    public void Set(Item item,int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.Icon;
    }

    private void Update()
    {
        //Reduce Time to live of gameObject
        ttl -= Time.deltaTime;

        //If time to live of gameObject = 0 then make it disapear
        if (ttl < 0)
        {
            Destroy(gameObject);
        }

        //calculate distance of item and player
        float distance = Vector2.Distance(transform.position, player.position);

        //Check if player is in pickUpRange of item
        if (distance > pickUpDistance)
        {
            return;
        }

        //Move item toward to player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        //If item is touch player then destroy item
        if (distance < 0.1f)
        {
            if (GameManagement.instance.itemContainer != null)
            {
                GameManagement.instance.itemContainer.Add(item, count);
            }
            Destroy(gameObject);
        }
    }

}
