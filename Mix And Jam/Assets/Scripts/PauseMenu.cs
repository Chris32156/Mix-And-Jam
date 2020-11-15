using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject htpButton;
    public GameObject htpCanvas;
    public GameObject pauseMenu;
    public GameObject unanimPauseMenu;
    SceneManagement scene;
    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<SceneManagement>();
        audio = FindObjectOfType<AudioManager>();
    }

    public void HowToPlayButton()
    {
        htpButton.transform.localScale = new Vector3(1f, 1f, 1f);
        htpCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        unanimPauseMenu.SetActive(false);
        audio.ButtonSound();
    }

    public void BackButton()
    {
        htpCanvas.SetActive(false);
        unanimPauseMenu.SetActive(true);
        audio.ButtonSound();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        scene.LoadScene("Main Menu");
        pauseMenu.SetActive(false);
        unanimPauseMenu.SetActive(false);
        audio.ButtonSound();
    }
}
