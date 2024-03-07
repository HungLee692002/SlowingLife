using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, Item item)
    {

        tileMapReadController.referenceManager.Place(item, tileMapPosition);

        return true;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer container)
    {
        container.Remove(usedItem);
    }
}
