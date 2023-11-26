using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float startTime;
    float currTime;
    TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currTime = Time.time - startTime;
        DisplayTime(currTime);

    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
