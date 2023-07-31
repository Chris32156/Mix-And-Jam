using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator transitions;
    string sceneName;

    public void LoadScene(string scene)
    {
        sceneName = scene;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitions.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
