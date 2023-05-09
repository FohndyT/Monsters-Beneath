using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private string CurrentScene;
    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(CurrentScene);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
