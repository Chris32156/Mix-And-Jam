using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceGenerator : MonoBehaviour
{
    public string[] sentences;
    int LastIndex = -1;
    public string GenerateSentence()
    {
        string sentence = sentences[RandomNumber()];
        return sentence + " ";
    }

    int RandomNumber()
    {
        int x = Random.Range(0, sentences.Length);
        if (x == LastIndex)
        {
            RandomNumber();
        }

        LastIndex = x;
        return x;
    }
}
