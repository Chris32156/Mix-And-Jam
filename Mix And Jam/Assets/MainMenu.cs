using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject AbilitiesCanvas;
    public GameObject BuildCanvas;
    public Animator MainMenuAnim;
    public Animator AbilitiesAnim;
    public Animator BuildAnim;

    bool pressed = false;
    bool pressed1 = false;
    bool pressed2 = false;

    SceneManagement scene;
    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<SceneManagement>();
        audio = FindObjectOfType<AudioManager>();

        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        if (!pressed)
        {
            PlayerPrefs.SetInt("StartingAmmount", 1);
            pressed = true;
            MainMenuAnim.SetTrigger("Fade Out");
            if (audio)
            {
                audio.PlayButtonSound();
            }
            Invoke("MainMenuFadeOut", 2);
        }
    }

    public void AbilitiesButton()
    {
        if (!pressed)
        {
            pressed = true;
            pressed1 = false;
            MainMenuAnim.SetTrigger("Fade Out");
            Invoke("AbilitiesFadeIn", 2);
        }
    }

    public void AbilitiesBackButton()
    {
        if (!pressed1)
        {
            pressed = false;
            pressed1 = true;

            AbilitiesAnim.SetTrigger("Fade Out");
            Invoke("AbilitiesFadeOut", 2);
        }
    }

    public void BuildButton()
    {
        if (!pressed)
        {
            pressed = true;
            pressed2 = false;
            MainMenuAnim.SetTrigger("Fade Out");
            Invoke("BuildFadeIn", 2);
        }
    }

    public void BuildBackButton()
    {
        if (!pressed2)
        {
            pressed = false;
            pressed2 = true;

            BuildAnim.SetTrigger("Fade Out");
            Invoke("BuildFadeOut", 2);
        }
    }

    void MainMenuFadeOut()
    {
        MainMenuCanvas.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    void AbilitiesFadeIn()
    {
        MainMenuCanvas.SetActive(false);
        AbilitiesCanvas.SetActive(true);
    }

    void BuildFadeIn()
    {
        MainMenuCanvas.SetActive(false);
        BuildCanvas.SetActive(true);
    }

    void BuildFadeOut()
    {
        MainMenuCanvas.SetActive(true);
        BuildCanvas.SetActive(false);
    }

    void AbilitiesFadeOut()
    {
        MainMenuCanvas.SetActive(true);
        AbilitiesCanvas.SetActive(false);
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
