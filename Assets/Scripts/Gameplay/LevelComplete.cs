using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel() // loads next level in build index
    {
        SceneManager.LoadScene(GetCurrentScene() + 1);
    }

    private int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
