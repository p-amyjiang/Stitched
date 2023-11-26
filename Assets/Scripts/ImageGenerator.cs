using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageGenerator : MonoBehaviour
{
    int currRow = 0;
    bool active = false;
    public GameObject emptyRowPrefab;
    public GameObject squarePrefab;
    public float rowSpeed;
    GameObject row;
    public Color[] colors;
    public int[][] soln_2 = new int[8][];
    public float transitionSpeed;

    public GameObject icon;

    [System.Serializable]
    public class ImageRow
    {
        public int[] row;
    }

    [SerializeField] 
    public ImageRow[] soln;

    [SerializeField]
    public string nextSquare;

    public AudioClip squareComplete;
    public AudioClip quiltComplete;

    void Start()
    {
        if (gameObject.name.Contains("-1"))
        {
            Debug.Log("im the starting square!");
            active = true;
        }
        //soln_2[0] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        //soln_2[1] = new int[] { 0, 0, 0, 1, 1, 1, 1, 0 };
        //soln_2[2] = new int[] { 0, 0, 1, 1, 1, 1, 0, 0 };
        //soln_2[3] = new int[] { 0, 0, 0, 2, 2, 2, 0, 0 };
        //soln_2[4] = new int[] { 0, 0, 0, 3, 3, 2, 0, 0 };
        //soln_2[5] = new int[] { 0, 0, 3, 3, 2, 2, 0, 0 };
        //soln_2[6] = new int[] { 0, 0, 3, 2, 2, 2, 0, 0 };
        //soln_2[7] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && active)
        {

            if (row != null)
            {
                row.GetComponent<Row>().stop();

                if (currRow == soln.Length)
                {
                    icon.SetActive(true);
                    Debug.Log("done, grabbing next square " + nextSquare);
                    if (nextSquare.Contains("End"))
                    {
                        AudioSource.PlayClipAtPoint(quiltComplete, transform.position);
                        StartCoroutine(TransitionNext());
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(squareComplete, transform.position);
                        active = false;
                        var next = GameObject.Find(nextSquare).GetComponent<ImageGenerator>();
                        StartCoroutine(next.GoToPos(Vector3.zero));
                    }
                    
                }
            }

            if (currRow < soln.Length)
            {
                int[] correctRow = soln[currRow].row;

                row = InstRow(correctRow, correctRow.Length/2 - currRow);

                currRow++;
                
            }
        }
    }



    IEnumerator TransitionNext ()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject obj = GameObject.Find("Quilt Complete");
        float startLoc = obj.GetComponent<CanvasGroup>().alpha;

        float elapsedTime = 0;
        float totalTransitionTime = (1f - startLoc) / 3f;

        while (obj.GetComponent<CanvasGroup>().alpha != 1f)
        {
            obj.GetComponent<CanvasGroup>().alpha = startLoc + (1f - startLoc) * (elapsedTime / totalTransitionTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        GoToScene(nextSquare);
    }

    public IEnumerator GoToPos(Vector3 endLoc)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 startLoc = transform.position;

        float elapsedTime = 0;
        float totalTransitionTime = Vector3.Distance(startLoc, endLoc) / transitionSpeed;

        while (transform.position != endLoc)
        {
            transform.position = Vector3.Lerp(startLoc, endLoc, elapsedTime / totalTransitionTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        active = true;
    }

    GameObject InstRow(int[] correctRow, int rowPos)
    {
        List<int> order = buildRowOrder(correctRow);
        int dir = 1 - 2*Mathf.Abs(rowPos % 2); //-1 for starting on left, 1 for starting on right

        row = Instantiate(emptyRowPrefab, new Vector2(1.5f*dir*correctRow.Length, rowPos-0.5f), Quaternion.identity);
        row.transform.SetParent(GetComponent<Transform>(), false);
        row.GetComponent<Row>().dir = dir * -1;
        row.GetComponent<Row>().front = true;
        row.GetComponent<Row>().order = order;

        for (int i = 0; i < order.Count; i++)
        {
            GameObject square = Instantiate(squarePrefab, new Vector2(i - order.Count/2, 0), Quaternion.identity);
            square.transform.SetParent(row.transform, false);
            square.GetComponent<SpriteRenderer>().color = colors[order[i]];
        }
        BoxCollider2D collider = row.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(order.Count, 1);
        collider.isTrigger = true;
        row.GetComponent<Rigidbody2D>().velocity = -1 * dir * rowSpeed * Vector2.right;

        GameObject dupRow = Instantiate(row, new Vector2(dir * (3.5f * correctRow.Length + 1), rowPos-0.5f), Quaternion.identity);
        dupRow.transform.SetParent(GetComponent<Transform>(), false);
        dupRow.GetComponent<Row>().front = false;
        dupRow.GetComponent<Rigidbody2D>().velocity = -1 * dir * rowSpeed * Vector2.right;

        row.GetComponent<Row>().dup = dupRow;
        dupRow.GetComponent<Row>().dup = row;
        return row;
    }

    List<int> buildRowOrder(int[] correctRow)
    {
        List<int> order = new() { };
        int correctIdx = Random.Range(0, correctRow.Length + 2);
        for (int i = 0; i <= correctRow.Length + 1; i++ )
        {
            if (i == correctIdx)
            {
                order.AddRange(correctRow);
            }
            else
            {
                order.Add(Random.Range(0, colors.Length));
            }
        }

        return order;
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
