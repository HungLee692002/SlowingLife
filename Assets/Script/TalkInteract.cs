using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    [SerializeField] DialogueContainer dialogueContainer;

    public override void Interact(Character character)
    {
        GameManagement.instance.dialogueSystem.Initialize(dialogueContainer);
    }
}
