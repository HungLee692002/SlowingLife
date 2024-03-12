using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public int id;

    public string Name;

    public bool Stackable;

    public Sprite Icon;

    public ToolAction onAction;

    public ToolAction onTileMapAction;

    public ToolAction onItemUsed;

    public Crop crop;

    public bool iconHighlight;

    public GameObject itemPrefab;
}
