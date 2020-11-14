using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{

    WordCheck wordCheck;
    Game game;

    private void Start()
    {
        wordCheck = FindObjectOfType<WordCheck>();
        game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!game.isPaused)
        {
            foreach (char letter in Input.inputString)
            {
                wordCheck.TypeLetter(letter);
            }
        }
    }
}