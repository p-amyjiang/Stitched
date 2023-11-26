using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour

{
    public GameObject pauseButton;
    public GameObject playButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Debug.Log("pause");
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void Play()
    {
        Debug.Log("play");
        Time.timeScale = 1;
        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
