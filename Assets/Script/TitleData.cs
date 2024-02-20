using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Data/Tile Data")]
public class TitleData : ScriptableObject
{
    public List<TileBase> tiles;

    public bool plowable;
}
