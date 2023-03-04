using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelObj, camObj;
    public static bool isPaused, gameOver;
    public AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                am.TurnOnMusic();
                ResumeGame();
                
            }
            else 
            {
                am.TurnOffMusic();
                PauseGame();
            }
        }
        if(gameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
            camObj.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pausePanelObj.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pausePanelObj.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public static void GameOver()
    {
        gameOver = true;
    }
}
