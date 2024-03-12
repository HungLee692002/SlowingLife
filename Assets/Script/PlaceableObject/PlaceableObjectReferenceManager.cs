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

    public bool check(Vector3Int positionOnGrid)
    {
        foreach (PlaceableObject po in objectManager.objectContainer.placeableObjects)
        {
            if (po.positionOnGrid == positionOnGrid)
            {
                return false;
            }
        }
        return true;
    }
}
