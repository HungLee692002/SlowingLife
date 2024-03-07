using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController,Item item=null)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(tileMapPosition);

        if(canPlow.Contains(tileToPlow) == false || tileMapReadController.cropManager.CheckTileStatus(tileMapPosition, 0))
        {
            return false;
        }

        tileMapReadController.cropManager.Plow(tileMapPosition);

        AudioManager.instance.Play(audioClip,volume);

        return true;
    }
}
