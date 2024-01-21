using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    CharacterController2D characterController;
    Rigidbody2D rgby2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1f;
    Character character;
    [SerializeField] HightLightController hightLightController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        rgby2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Check();

        //Get mouse input to cut tree
        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }

    private void Check()
    {
        Vector2 position = rgby2d.position + characterController.lastMotionVector * offsetDistance;
        position.y -= 0.5f;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in collider2Ds)
        {
            Interactable action = c.GetComponent<Interactable>();
            if (action != null)
            {
                hightLightController.HightLight(action.gameObject);
                return;
            }
        }

        hightLightController.Hide();
    }

    private void Interact()
    {

        Vector2 position = rgby2d.position + characterController.lastMotionVector * offsetDistance;
        position.y -= 0.5f;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in collider2Ds)
        {
            Interactable action = c.GetComponent<Interactable>();
            if (action != null)
            {
                action.Interact(character);
                break;
            }
        }
    }
}
