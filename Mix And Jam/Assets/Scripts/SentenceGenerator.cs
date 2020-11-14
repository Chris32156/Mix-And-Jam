using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceGenerator : MonoBehaviour
{
    public string[] sentences;
    public string GenerateSentence()
    {
        string sentence = sentences[Random.Range(0, sentences.Length)];
        return sentence + " ";
    }
}
