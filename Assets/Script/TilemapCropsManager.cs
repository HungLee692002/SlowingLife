using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;

    [SerializeField] TileBase seeded;

    [SerializeField] TileBase watered;

    Tilemap targetTileMap;

    [SerializeField] GameObject cropSpritePrefab;

    [SerializeField] CropContainer container;


    private void Start()
    {
        GameManagement.instance.GetComponent<CropManager>().cropsManager = this;
        
        targetTileMap = GetComponent<Tilemap>();
        SetCrop();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        if (targetTileMap == null) { return; }

        foreach (CropTile cropTile in container.crops)
        {
            if (cropTile.crop != null)
            {
                if (cropTile.complete)
                {
                    Debug.Log("I'm done growing");
                    continue;
                }

                cropTile.growTimer += 1;

                if (cropTile.growTimer >= cropTile.crop.growStageTime[cropTile.growStage] && cropTile.isWatered)
                {
                    cropTile.growStage += 1;
                    cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                }


            }
        }
    }

    public void Plow(Vector3Int position)
    {

        //plow the tile
        CreatePlowedTile(position);
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        if (targetTileMap == null) { return; }

        CropTile crop = new CropTile();

        crop.isPlowed = true;

        crop.position = position;
        //add tile that being plow to dictionary
        container.Add(crop);

        //set tile sprite to plowed
        targetTileMap.SetTile(position, plowed);
    }


    public void WateredTile(Vector3Int position)
    {
        if (targetTileMap == null) { return; }

        CropTile crop = container.Get(position);

        if (crop == null) { return; }  

        crop.isWatered = true;

        //set tile sprite to watered
        targetTileMap.SetTile(position, watered);

    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if (tile == null) { return; }

        //plow the tile
        SeededTile(tile, toSeed);
    }

    private void SeededTile(CropTile tile, Crop toSeed)
    {
        tile.isSeeded = true;
        tile.crop = toSeed;
        tile.growStage = 0;
        tile.growTimer = 1;

        GameObject go = Instantiate(cropSpritePrefab);
        go.transform.position = targetTileMap.CellToWorld(tile.position);
        go.SetActive(false);
        tile.spriteRenderer = go.GetComponent<SpriteRenderer>();

        tile.spriteRenderer.gameObject.SetActive(true);
        tile.spriteRenderer.sprite = tile.crop.sprites[tile.growStage];

    }

    internal void PickUp(Vector3Int tileMapPosition)
    {
        if (targetTileMap == null) { return; }

        Vector2Int position = (Vector2Int)tileMapPosition;

        CropTile tile = container.Get(tileMapPosition);

        if (tile == null) { return; }

        if (tile.complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                targetTileMap.CellToWorld(tileMapPosition),
                tile.crop.yield,
                tile.crop.count);

            tile.Harvested();
        }

    }

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    internal bool CheckTileStatus(Vector3Int tileMapPosition, int option)
    {
        switch (option)
        {
            case 0:
                {
                    return container.Get(tileMapPosition) != null;
                }
            case 1:
                {
                    return container.Get(tileMapPosition) != null && container.Get(tileMapPosition).isSeeded == false;
                }
            case 2:
                {
                    return container.Get(tileMapPosition) != null && container.Get(tileMapPosition).isWatered == false;
                }
            default: return false;
        }
    }

    public void SetCrop()
    {
        string currentActiveScene = SceneManager.GetActiveScene().name;

        string targetScene = gameObject.scene.name;

        if(currentActiveScene != targetScene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene));
        }

        if (container != null)
        {
            foreach (CropTile cropTile in container.crops)
            {
                if (cropTile != null)
                {
                    if (cropTile.isPlowed)
                    {
                        targetTileMap.SetTile(cropTile.position, plowed);

                    }
                    if(cropTile.isWatered)
                    {
                        targetTileMap.SetTile(cropTile.position, watered);

                    }
                    if(cropTile.isSeeded)
                    {
                        GameObject go = Instantiate(cropSpritePrefab);
                        go.transform.position = targetTileMap.CellToWorld(cropTile.position);
                        go.SetActive(false);
                        cropTile.spriteRenderer = go.GetComponent<SpriteRenderer>();

                        cropTile.spriteRenderer.gameObject.SetActive(true);
                        cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    }
                }
            }
        }
    }
}
