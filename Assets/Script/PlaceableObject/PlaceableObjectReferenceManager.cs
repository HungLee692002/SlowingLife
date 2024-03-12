using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectReferenceManager : MonoBehaviour
{
    public PlaceableObjectManager objectManager;

    public void Place(Item item, Vector3Int pos)
    {
        if(objectManager == null)
        {
            Debug.Log("No placeableObjectManager in this scene");
            return;
        }

        objectManager.Place(item, pos);
    }
}
