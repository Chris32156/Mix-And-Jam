using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject htpButton;
    public GameObject mainMenu;
    public GameObject htpCanvas;
    public GameObject back;
    SceneManagement scene;
    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<SceneManagement>();
        audio = FindObjectOfType<AudioManager>();
    }

    public void PlayButton()
    {
        scene.LoadScene("Game");
        if (audio)
        {
            audio.PlayButtonSound();
        }
    }

    public void HowToPlay()
    {
        htpCanvas.SetActive(true);
        mainMenu.SetActive(false);
        htpButton.transform.localScale = new Vector3(1f, 1f, 1f);
        audio.ButtonSound();
    }

    public void Back()
    {
        htpCanvas.SetActive(false);
        mainMenu.SetActive(true);
        back.transform.localScale = new Vector3(1f, 1f, 1f);
        audio.ButtonSound();
    }

    public void QuitToMainMenu()
    {
        scene.LoadScene("Main Menu");
        audio.ButtonSound();
    }

    public void Quit()
    {
        Application.Quit();
        audio.ButtonSound();
    }
}
