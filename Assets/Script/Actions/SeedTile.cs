using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, Item item)
    {
        if(tileMapReadController.cropManager.CheckTileStatus(tileMapPosition,1) == false)
        {
            return false;
        }


        tileMapReadController.cropManager.Seed(tileMapPosition,item.crop);

        return true ;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }
}
