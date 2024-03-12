using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaceableObject
{
    public Item placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;

    /// <summary>
    /// serialized JSON string which contains the state of the object 
    /// </summary>
    public string objectState;

    public PlaceableObject(Item item,Transform target, Vector3Int position)
    {
        placedItem = item;

        targetObject = target;

        positionOnGrid = position;
    }
}

[CreateAssetMenu(menuName ="Data/Placeable Objects Container")]
public class PlaceableObjectContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;
}
