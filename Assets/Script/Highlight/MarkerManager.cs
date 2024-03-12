using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTileMap;

    [SerializeField] TileBase tileBase;

    public Vector3Int markedCellPosition;

    Vector3Int oldCellPosition;

    bool show;

    private void Update()
    {
        if(show)
        {
            targetTileMap.SetTile(oldCellPosition, null);
            targetTileMap.SetTile(markedCellPosition, tileBase);
            oldCellPosition = markedCellPosition;
        }  
    }

    internal void Show(bool selectable)
    {
        show = selectable;
        targetTileMap.gameObject.SetActive(show);
    }
}
