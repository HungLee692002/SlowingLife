using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayNightSystem : MonoBehaviour
{
    public float duration = 8640f;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Light2D _light;

    [SerializeField] private Text timeText;
    private float time;

    public void Awake()
    {
        time = Time.time;
    }

    public void Update()
    {
        time += Time.deltaTime;
        float fraction = Mathf.Abs(Mathf.Sin(time / duration * Mathf.PI));

        // Evaluate the gradient at the current fraction to get the color.
        Color color = gradient.Evaluate(fraction);

        // Apply the color to the material.
        _light.color = color;
        Debug.Log(time);

        //UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        int fastForwardTime = (int)(time * 10);

        // Định dạng thời gian để hiển thị.
        int minutes = (int)(fastForwardTime / 60) % 60;

        int hours = (int)(fastForwardTime / 3600) % 24;
        if(minutes %10 == 0)
        {
            timeText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);

        }
    }
}
