using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    private void Awake()
    {
        instance = this; 
    }

    public GameObject player;

}