using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1);
    }

    internal void InitiateTransistion(Transform toTransistion)
    {
        toTransistion.position = destination.position;
    }
}
