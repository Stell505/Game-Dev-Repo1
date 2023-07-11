using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, Interactable
{
    public string SceneName;

    public void Interact()
    {
        StartCoroutine(LevelLoader.Instance.LoadLevel(SceneName));
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            StartCoroutine(LevelLoader.Instance.LoadLevel(SceneName));
        }
    }
}
