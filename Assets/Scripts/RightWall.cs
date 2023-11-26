using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var row = collision.GetComponent<Row>();

        if (row.dir < 0 && !row.front)
        {
            Debug.Log("leaving right side");
            row.dup.transform.position = new Vector2(collision.transform.position.x - row.dir * row.order.Count, row.dup.transform.position.y);

            row.front = true;
            row.dup.GetComponent<Row>().front = false;
        }
    }
}
