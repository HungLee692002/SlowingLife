using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController)
    {
        if(tileMapReadController.cropManager.CheckPlow(tileMapPosition) == false)
        {
            return false;
        }


        tileMapReadController.cropManager.Seed(tileMapPosition);

        return true ;
    }
}
