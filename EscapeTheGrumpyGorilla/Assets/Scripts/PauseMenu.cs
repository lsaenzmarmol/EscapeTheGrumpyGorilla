using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelObj;
    public static bool isPaused;
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
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
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
}
