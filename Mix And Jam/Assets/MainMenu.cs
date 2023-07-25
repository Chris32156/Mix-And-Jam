using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject AbilitiesCanvas;
    public Animator MainMenuAnim;
    public Animator AbilitiesAnim;

    bool pressed = false;
    bool pressed1 = false;

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
        if (!pressed)
        {
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
