using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public bool isPlowed = false;

    public bool isSeeded = false;

    public bool isWatered = false;

    public int growTimer;

    public int growStage;

    public bool complete
    {
        get
        {
            if(crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    public Crop crop;

    public SpriteRenderer spriteRenderer;

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        spriteRenderer.gameObject.SetActive(false);
    }
}


public class CropManager : TimeAgent
{
    [SerializeField] TileBase plowed;

    [SerializeField] TileBase seeded;

    [SerializeField] TileBase watered;

    [SerializeField] Tilemap targetTileMap;

    Tilemap TargetTileMap
    {
        get
        {
            if(targetTileMap == null)
            {
                targetTileMap = GameObject.Find("PlowedTileMap").GetComponent<Tilemap>();
            }
            return targetTileMap;
        }
    }

    [SerializeField] GameObject cropSpritePrefab;

    [SerializeField] ToolBarController toolBarController;

    Dictionary<Vector2Int, CropTile> cropTiles;


    private void Start()
    {
        //dictionary to store which tile is plowed with key is position of that tile
        cropTiles = new Dictionary<Vector2Int, CropTile>();

        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        if (TargetTileMap == null) { return; }

        foreach (CropTile cropTile in cropTiles.Values)
        {
            if (cropTile.crop != null)
            {
                if (cropTile.complete)
                {
                    Debug.Log("I'm done growing");
                    continue;
                }

                cropTile.growTimer += 1;

                if (cropTile.growTimer >= cropTile.crop.growStageTime[cropTile.growStage] && cropTile.isWatered )
                {
                    cropTile.growStage += 1;
                    cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                }

                
            }
        }
    }

    public bool CheckTileStatus(Vector3Int position, int option)
    {
        if (TargetTileMap == null) { return false; }

        switch (option)
        {
            case 0:
                {
                    return cropTiles.ContainsKey((Vector2Int)position) ;
                }
            case 1:
                {
                    return cropTiles.ContainsKey((Vector2Int)position) && cropTiles[(Vector2Int)position].isSeeded == false;
                }
            case 2:
                {
                    return cropTiles.ContainsKey((Vector2Int)position) && cropTiles[(Vector2Int)position].isWatered == false;
                }
            default: return false;
        }

    }

    public void Plow(Vector3Int position)
    {
        //check if is tile is plow or not
        if (CheckTileStatus(position, 0))
        {
            return;
        }

        //plow the tile
        CreatePlowedTile(position);
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        if (TargetTileMap == null) { return; }

        CropTile crop = new CropTile();

        crop.isPlowed = true;

        //add tile that being plow to dictionary
        cropTiles.Add((Vector2Int)position, crop);

        //set tile sprite to plowed
        TargetTileMap.SetTile(position, plowed);
    }


    public void WateredTile(Vector3Int position)
    {
        if (TargetTileMap == null) { return; }

        if (!CheckTileStatus(position, 2)) { return; }

        cropTiles[(Vector2Int)position].isWatered = true;

        //set tile sprite to watered
        TargetTileMap.SetTile(position, watered);

    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        //check if is tile is seeded or not
        if (!CheckTileStatus(position, 1))
        {
            return;
        }

        //plow the tile
        SeededTile(position, toSeed);
    }

    private void SeededTile(Vector3Int position, Crop toSeed)
    {
        CropTile crop = cropTiles[(Vector2Int)position];
        crop.isSeeded = true;
        crop.crop = toSeed;
        crop.growStage = 0;
        crop.growTimer = 1;

        GameObject go = Instantiate(cropSpritePrefab);
        go.transform.position = TargetTileMap.CellToWorld(position);
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();

        crop.spriteRenderer.gameObject.SetActive(true);
        crop.spriteRenderer.sprite = crop.crop.sprites[crop.growStage];

    }

    internal void PickUp(Vector3Int tileMapPosition)
    {
        if(TargetTileMap == null) { return; }

        Vector2Int position = (Vector2Int)tileMapPosition;
        if (cropTiles.ContainsKey(position))
        {
            CropTile crop = cropTiles[position];

            if(crop.complete)
            {
                ItemSpawnManager.instance.SpawnItem(
                    TargetTileMap.CellToWorld(tileMapPosition)
                    ,crop.crop.yield
                    ,crop.crop.count);

                crop.Harvested();
            }
        }
    }
}
