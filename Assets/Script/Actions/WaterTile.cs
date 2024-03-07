using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Water Tile")]
public class WaterTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController,Item item)
    {
        if (tileMapReadController.cropManager.CheckTileStatus(tileMapPosition, 2) == false)
        {
            return false;
        }

        tileMapReadController.cropManager.WateredTile(tileMapPosition);

        AudioManager.instance.Play(audioClip,volume);

        return true;
    }
}
