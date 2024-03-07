using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChestInteract : Interactable
{
    [SerializeField] GameObject openChest;
    [SerializeField] GameObject closeChest;
    [SerializeField] bool opened;
    [SerializeField] AudioClip openChestAudioClip;
    [SerializeField] AudioClip closeChestAudioClip;

    public override void Interact(Character character)
    {
        if (opened == false)
        {
            opened = true;
            openChest.SetActive(true);
            closeChest.SetActive(false);

            AudioManager.instance.Play(openChestAudioClip);
        } else
        {
            opened = false;
            openChest.SetActive(false);
            closeChest.SetActive(true);

            AudioManager.instance.Play(closeChestAudioClip);

        }

    }

}
