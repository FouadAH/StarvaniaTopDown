using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public KeySO keyData;
    public CanvasGroup canvasGroup;
    bool isInRange;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnOpen();
            }
        }
    }

    public void OnOpen()
    {
        if (keyData == null)
            return;

        if (!keyData.hasBeenCollected)
            return;

        //Load Next Scene
        Debug.Log("Door Opened"); 
        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(levelIndex);

        //if (SceneManager.GetSceneByBuildIndex(levelIndex).IsValid())
        //{
        //    Debug.Log($"Load Scene with index {levelIndex}");
        //    SceneManager.LoadScene(levelIndex);
        //}
        //else
        //{
        //    Debug.LogWarning($"Scene with index {levelIndex} not found!");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            canvasGroup.alpha = 1;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            canvasGroup.alpha = 0;
            isInRange = false;
        }
    }
}
