using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject GoldCanvas;

    AudioManager audio;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (audio)
            {
            }

            Time.timeScale = 0;
            GoldCanvas.SetActive(true);
        }
    }

    private void RandomizePosition()
    {
        //Declare Vars
        int xModifier;
        int yModifier;
        float x = Random.Range(2f, 5f);
        float y = Random.Range(2f, 5f);
        xModifier = Random.Range(0, 2);
        yModifier = Random.Range(0, 2);

        if (xModifier == 1)
        {
            x = x * -1;
        }
        if (yModifier == 1)
        {
            y = y * -1;
        }

        x += player.transform.localPosition.x;
        y += player.transform.localPosition.y;
        transform.position = new Vector2(x, y);
    }

    public void WatchAd()
    {
        FindObjectOfType<AdsManager>().LoadAd(1000);
    }
}
