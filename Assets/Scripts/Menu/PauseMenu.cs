using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool EnPause = false;
    public GameObject PauseMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.M))
        {
            if(EnPause)
            {
                Jouer();
            }
            else
            {
                Stop();
            }
        }
    }
    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        EnPause = true;
    }
    public void Jouer()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        EnPause = false;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
