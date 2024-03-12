using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;

    Vector3 targetPosition;

    [SerializeField] Tilemap targetTilemap;

    SpriteRenderer spriteRenderer;

    bool canSelect;

    bool show;

    public bool CanSelect
    {
        set
        {
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    public bool Show
    {
        set
        {
            show = value; 
            gameObject.SetActive(canSelect && show);
        }
    }

    private void Start()
    {
        //targetTilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (targetTilemap == null) { return; }
        targetPosition = targetTilemap.CellToWorld(cellPosition);

        Vector2 pivot = spriteRenderer.sprite.pivot;

        if(pivot.x == 0f && pivot.y == 0f)
        {
            transform.position = targetPosition;

        } else
        {
            transform.position = targetPosition + targetTilemap.cellSize / 2;
        }

    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = icon;
    }
}
