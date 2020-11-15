using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip Button;
    public AudioClip PlayButton;
    public AudioClip Hit;
    public AudioClip PlayerHit;
    public AudioClip GameOver;

    AudioSource audio;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        var objs = FindObjectsOfType<AudioManager>();

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ButtonSound()
    {
        audio.PlayOneShot(Button);
    }

    public void PlayButtonSound()
    {
        audio.PlayOneShot(PlayButton);
    }

    public void HitSound()
    {
        audio.PlayOneShot(Hit);
    }

    public void PlayerHitSound()
    {
        audio.PlayOneShot(PlayerHit);
    }

    public void GameOverSound()
    {
        audio.PlayOneShot(GameOver);
    }
}
