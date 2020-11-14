using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{

    WordCheck wordCheck;

    private void Start()
    {
        wordCheck = FindObjectOfType<WordCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            wordCheck.TypeLetter(letter);
        }
    }
}