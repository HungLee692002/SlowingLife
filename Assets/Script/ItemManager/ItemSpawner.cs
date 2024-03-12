using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item toSpawn;
    [SerializeField] int howMany;

    [SerializeField] float spread = 0.75f;

    [SerializeField] float probability = 0.5f;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += Spawn;
    }

    void Spawn()
    {
        if(UnityEngine.Random.value < probability) 
        {

            Vector3 postion = transform.position;
            postion.x += spread * UnityEngine.Random.value - spread / 2;
            postion.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(postion, toSpawn, howMany);
        }
    }
}
