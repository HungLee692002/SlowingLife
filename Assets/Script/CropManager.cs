using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{

}


public class CropManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;

    [SerializeField] TileBase seeded;

    [SerializeField] TileBase water;

    [SerializeField] Tilemap targetTileMap;
    
    Dictionary<Vector2Int,Crops> crops;

    private void Start()
    {
        //dictionary to store which tile is plowed with key is position of that tile
        crops = new Dictionary<Vector2Int, Crops> ();
    }

    public bool CheckPlow(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        //check if is tile is plow or not
        if(CheckPlow(position))
        {
            return;
        }

        //plow the tile
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position)
    {
        //set tile sprite to seeded
        targetTileMap.SetTile(position, seeded);
    }

    public void Water(Vector3Int position)
    {
        targetTileMap.SetTile(position, water);
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops ();

        //add tile that being plow to dictionary
        crops.Add((Vector2Int)position, crop);

        //set tile sprite to plowed
        targetTileMap.SetTile(position, plowed);
    }
}
