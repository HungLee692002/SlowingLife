using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;

    private void Awake()
    {
        player = GameManagement.instance.player.transform;
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
            Destroy(gameObject);
        }
    }

}
