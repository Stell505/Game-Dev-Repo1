using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public static LevelLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void loadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    public IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
