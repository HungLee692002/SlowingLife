using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsChacterController : MonoBehaviour
{
    CharacterController2D characterController2D;

    Rigidbody2D rgby2d;

    ToolBarController toolBarController;

    Animator animator;

    [SerializeField] float offsetDistance = 1f;

    [SerializeField] MarkerManager markerManager;

    [SerializeField] TileMapReadController tileMapReadController;

    [SerializeField] float maxDistance = 1.5f;

    [SerializeField] ToolAction onTilePickUp;

    Vector3Int selectedTilePosition;

    bool selectable;

    private void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
        rgby2d = GetComponent<Rigidbody2D>();
        toolBarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectedTile();
        CanSelectCheck();
        Marker();

        //Get mouse input to cut tree
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld())
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void SelectedTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    void CanSelectCheck()
    {
        //get character position
        Vector2 characterPosition = transform.position;

        //get mouse position
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //check if mouse is near character
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;

        //show or hide marker depend on value above
        markerManager.Show(selectable);
    }

    private bool UseToolWorld()
    {
        //get offset postion of character where character is facing
        Vector2 position = rgby2d.position + characterController2D.lastMotionVector * offsetDistance;
        position.y -= 0.5f;

        Item item = toolBarController.GetItem;

        if (item == null)
        {
            return false;
        }

        if (item.onAction == null )
        {
            return false;
        }

        bool compltet = item.onAction.OnApply(position);

        return false;
    }

    private void UseToolGrid()
    {
        //check if mouse is near player to select tile
        if (selectable)
        {
            Item item = toolBarController.GetItem;
            if(item == null) 
            {
                PickUpTile();
                return; 
            }
            if (item.onTileMapAction == null) { return; }

            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController,item);

            if (complete)
            {
                if(item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManagement.instance.itemContainer);
                }
            }
        }
    }

    private void PickUpTile()
    {
        if(onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
