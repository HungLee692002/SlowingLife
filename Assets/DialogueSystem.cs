using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;

    [SerializeField] Text nameText;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;

    [SerializeField] float timePerLetter = 0.05f;

    float totalTimeToType, currentTime;

    string lineToShow;

    DialogueContainer currentDialogue;

    int currentTextLine;

    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PushText();
            }
            TypeOutText();
        }
        
    }

    private void TypeOutText()
    {
        if(currentTime >= totalTimeToType) { return; }
        currentTime += Time.deltaTime;
        float progress = currentTime / totalTimeToType;
        progress = Mathf.Clamp(progress,0,1f);
        int letterCount = (int)(lineToShow.Length * progress);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {
        currentTextLine += 1;

        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        } else
        {
            lineToShow = currentDialogue.line[currentTextLine];
            totalTimeToType = lineToShow.Length * timePerLetter;
            currentTime = 0f;
            targetText.text = "";
        }
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentTextLine = -1;
        PushText();
    }

    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }

    private void Conclude()
    {
        Debug.Log("Error");
        Show(false);
    }
}