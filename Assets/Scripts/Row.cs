using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public int dir;
    public bool front;
    public GameObject dup;
    public List<int> order;

    public AudioClip barLock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        dup.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        AudioSource.PlayClipAtPoint(barLock, transform.position, 3);
    }
}
