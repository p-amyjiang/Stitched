using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
