using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public static bool Paused = false;
    


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public GameObject pauseMenu;
    public void Resume()
    {
        if(FindObjectOfType<ThemeMusic>() != null)
            FindObjectOfType<ThemeMusic>().GetComponent<AudioSource>().Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1F;
        Paused = false;
        Cursor.visible = false;

        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1F;
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();
        
    }

    void Pause()
    {
        Cursor.visible = true;
        if(FindObjectOfType<ThemeMusic>() != null)
            FindObjectOfType<ThemeMusic>().GetComponent<AudioSource>().Pause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0F;
        Paused = true;
    }
}