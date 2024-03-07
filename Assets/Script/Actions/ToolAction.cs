using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume =1f;

    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController,Item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    public virtual void OnItemUsed(Item usedItem,ItemContainer container)
    {

    }

}
