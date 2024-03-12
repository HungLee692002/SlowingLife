using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class PlaceableObjectManager : MonoBehaviour
{
    public PlaceableObjectContainer objectContainer;

    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManagement.instance.GetComponent<PlaceableObjectReferenceManager>().objectManager = this;
        SetObjectToMap();
    }
    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if(targetTilemap == null) { return; }

        GameObject go = Instantiate(item.itemPrefab);

        Vector3 position = targetTilemap.CellToWorld(positionOnGrid);

        go.transform.position = position;


        objectContainer.placeableObjects.Add(new PlaceableObject(item, go.transform, positionOnGrid));

    }

    public void SetObjectToMap()
    {
        string currentActiveScene = SceneManager.GetActiveScene().name;

        string targetScene = gameObject.scene.name;

        if (currentActiveScene != targetScene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene));
        }

        if(objectContainer != null)
        {
            foreach(PlaceableObject p in objectContainer.placeableObjects)
            {
                GameObject go = Instantiate(p.placedItem.itemPrefab);

                Vector3 position = targetTilemap.CellToWorld(p.positionOnGrid);

                go.transform.position = position;

                p.targetObject = go.transform;

                IPersistant persistant = go.GetComponent<IPersistant>();
                if(persistant != null)
                {
                    persistant.Load(p.objectState);
                }
            }
        }
    }

    private void Update()
    {
        for(int i = 0;i < objectContainer.placeableObjects.Count; i++)
        {
            IPersistant persistant = objectContainer.placeableObjects[i].targetObject.GetComponent<IPersistant>();

            if (persistant != null)
            {
                string jsonString = persistant.Read();
                objectContainer.placeableObjects[i].objectState = jsonString;
            }
        }
    }


}
