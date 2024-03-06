using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CropTile
{
    public bool isPlowed = false;

    public bool isSeeded = false;

    public bool isWatered = false;

    public int growTimer;

    public int growStage;

    public Vector3Int position;

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
        isSeeded = false;
        spriteRenderer.gameObject.SetActive(false);
    }
}


public class CropManager : MonoBehaviour
{
    public TilemapCropsManager cropsManager;



    public void PickUp(Vector3Int position)
    {
        if(cropsManager == null) { return; }

        cropsManager.PickUp(position);
    }
    
    public bool Check(Vector3Int position)
    {
        return cropsManager.Check(position);
    }

    internal void Seed(Vector3Int tileMapPosition, Crop crop)
    {
        if (cropsManager == null) { return; }

        cropsManager.Seed(tileMapPosition, crop);
    }

    internal void WateredTile(Vector3Int tileMapPosition)
    {
        if (cropsManager == null) { return; }

        cropsManager.WateredTile(tileMapPosition);

    }

    internal void Plow(Vector3Int tileMapPosition)
    {
        if (cropsManager == null) { return; }

        cropsManager.Plow(tileMapPosition);
    }

    internal bool CheckTileStatus(Vector3Int tileMapPosition, int num)
    {
        return cropsManager.CheckTileStatus(tileMapPosition, num);
    }
}
