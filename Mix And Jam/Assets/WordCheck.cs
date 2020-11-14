using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WordCheck : MonoBehaviour
{

    string sentence;

    private List<string> words = new List<string>();
    private int activeWord;
    int currentLetter = 0;
    int currentWord = 0;
    int numOfWords;

    SentenceGenerator sentenceGenerator;
    public TextMeshProUGUI sentenceText;
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI wordText2;

    void Start()
    {
        sentenceGenerator = FindObjectOfType<SentenceGenerator>();

        GenerateSentence();

        for (int i = 0; i < words.Count; i++)
        {
            Debug.Log(words[i]);
        }
    }

    public void GenerateSentence()
    {
        numOfWords = 0;
        currentWord = 0;
        sentence = sentenceGenerator.GenerateSentence();
        sentenceText.SetText(sentence);
        GenerateWords();
        UpdateText();
    }

    public void GenerateWords()
    {
        if (words != null)
        {
            words.Clear();
        }

        int index = 0;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] == ' ')
            {
                words.Add(sentence.Substring(index, i - index).ToLower());
                index = i + 1;
                numOfWords++;
            }
        }
    }

    public void TypeLetter(char letter)
    {
        Debug.Log(letter);
        if (letter == words[currentWord][currentLetter])
        {
                currentLetter++;
                UpdateText();
                Debug.Log("Correct Letter");

            // If Word Is Done
            if (currentLetter >= words[currentWord].Length)
            {
                currentLetter = 0;
                currentWord++;
                Debug.Log("Correct Word");

                // If Sentence Is done
                if (currentWord >= numOfWords)
                {
                    GenerateSentence();
                    Debug.Log("Correct Sentence");
                }
                else
                {
                    UpdateText();
                }
            }
        }
    }

    void UpdateText()
    {
        Debug.Log(words[currentWord].Length);
        Debug.Log("Current Letter :" + currentLetter.ToString());
        wordText.SetText(words[currentWord].Substring(currentLetter, words[currentWord].Length - currentLetter));
        wordText2.SetText(words[currentWord].Substring(currentLetter, words[currentWord].Length - currentLetter));
    }
}
