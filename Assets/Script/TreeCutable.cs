using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeCutable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount ;
    [SerializeField] float spread = 0.75f;

    public void Awake()
    {
        dropCount = Random.Range(3, 6);
    }

    public override void Hit()
    {
        while(dropCount > 0)
        {
            Debug.Log("Value of variable: " + dropCount);
            dropCount--;

            Vector3 postion = transform.position;
            postion.x += spread * UnityEngine.Random.value - spread / 2;
            postion.y += spread * UnityEngine.Random.value - spread / 2;

            GameObject go = Instantiate(pickUpDrop);

            go.transform.position = postion;
        }
        //Destroy when play hit
        Destroy(gameObject);
    }

}
