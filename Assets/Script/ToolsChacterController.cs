using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsChacterController : MonoBehaviour
{
    CharacterController2D characterController2D;

    Rigidbody2D rgby2d;

    [SerializeField] float offsetDistance = 1f;

    [SerializeField] float sizeOfInteractableArea = 1f;

    [SerializeField] MarkerManager markerManager;

    [SerializeField] TileMapReadController tileMapReadController;

    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
        rgby2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Marker();

        //Get mouse input to cut tree
        if(Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void Marker()
    {
        Vector3Int gridPosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        markerManager.markedCellPosition = gridPosition;
    }

    private void UseTool()
    {
        Vector2 position = rgby2d.position + characterController2D.lastMotionVector*offsetDistance;
        position.y -= 0.5f;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach(Collider2D c in collider2Ds)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
