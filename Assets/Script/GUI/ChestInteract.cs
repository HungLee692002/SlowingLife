using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChestInteract : Interactable
{
    [SerializeField] bool opened;
    [SerializeField] AudioClip openChestAudioClip;
    [SerializeField] AudioClip closeChestAudioClip;
    [SerializeField] ItemContainer itemContainer;
    public override void Interact(Character character)
    {
        if (opened == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }


    }

    public void Open(Character character)
    {
        opened = true;

        AudioManager.instance.Play(openChestAudioClip);

        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        opened = false;

        AudioManager.instance.Play(closeChestAudioClip);

        character.GetComponent<ItemContainerInteractController>().Close();
    }
}
