using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayNightSystem : MonoBehaviour
{
    public float duration = 8640f;

    public float phaseLenght = 10f;

    int currentPhase = 0;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Light2D _light;

    [SerializeField] private Text timeText;

    [SerializeField] private float startAtTime = 2880f; //start at 08:00 AM

    public int currentDay = 1;

    public int currentMonth = 1;

    public int currentYear = 1;

    private float time;

    List<TimeAgent> timeAgent;

    public void Awake()
    {
        timeAgent = new List<TimeAgent>();
        //time = Time.time;
    }

    private void Start()
    {
        time = startAtTime;
    }

    public void Subscribe(TimeAgent time)
    {
        timeAgent.Add(time);
    }

    public void UnSubscribe(TimeAgent time)
    {
        timeAgent.Remove(time);
    }

    public void Update()
    {
        time += Time.deltaTime;
        float fraction = Mathf.Abs(Mathf.Sin((time - startAtTime) / duration * Mathf.PI));

        // Evaluate the gradient at the current fraction to get the color.
        Color color = gradient.Evaluate(fraction);

        // Apply the color to the material.
        _light.color = color;
        //Debug.Log(time);

        UpdateTimeDisplay();
        TimeAgents();
    }

    private void TimeAgents()
    {
        int phase = (int)(time / phaseLenght);
        Debug.Log(time + " - " + phaseLenght);
        Debug.Log(currentPhase + " - " + phase);
        if (currentPhase != phase)
        {
            currentPhase = phase;
            for (int i = 0; i < timeAgent.Count; i++)
            {
                timeAgent[i].Invoke();
            }
        }

    }

    void UpdateTimeDisplay()
    {
        int fastForwardTime = (int)(time * 10);

        // Định dạng thời gian để hiển thị.
        int minutes = (int)(fastForwardTime / 60) % 60;

        int hours = (int)(fastForwardTime / 3600) % 24;
        if (minutes % 10 == 0)
        {
            timeText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);

        }

        //Check if a day end at 02:00 AM
        if (minutes == 0 && hours == 2)
        {
            time = startAtTime;
            currentDay++;
        }

        //Check if a month end after 30 days
        if (currentDay > 30)
        {
            currentMonth++;
            currentDay = 1;
        }

        //Check if a year end after 4 months
        if (currentMonth > 4) 
        {
            currentYear++;
            currentMonth = 1;
        }
        //Debug.Log("Time: " + string.Format("{0:D2}:{1:D2}", hours, minutes));
    }
}
