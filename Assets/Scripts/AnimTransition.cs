using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimTransition : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToScore()
    {
        string nextScene = SceneManager.GetActiveScene().name + "Score";
        StartCoroutine(GoToScoreCR(nextScene));
    }

    IEnumerator GoToScoreCR(string nextScene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(nextScene);
    }
}
