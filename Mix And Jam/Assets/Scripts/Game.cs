﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Params
    [SerializeField] GameObject PauseMenuCanvas;
    [SerializeField] GameObject UnanimatedCanvas;
    [SerializeField] GameObject SettingsMenuCanvas;
    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] Animator PauseMenuAnim;

    // Declare Vars
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle Pause
            if (!isPaused)
            {
                Time.timeScale = 0;
                PauseMenuCanvas.SetActive(true);
                isPaused = true;

                PauseMenuAnim.SetTrigger("PauseMenuFadeIn");
            }
            // If Already Paused
            else
            {
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        PauseMenuCanvas.SetActive(false);
        UnanimatedCanvas.SetActive(false);
        SettingsMenuCanvas.SetActive(false);
        isPaused = false;

        //Reset Animation
        PauseMenuPanel.transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }
}