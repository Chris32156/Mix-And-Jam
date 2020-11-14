using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TypingManager : MonoBehaviour
{
    public List<Word> words;
    public TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string input = Input.inputString.ToLower();

        if (input.Equals(""))
            return;

        char c = input[0];
        string typing = "";
        for (int i = 0; i < words.Count; i++)
        {
            Word w = words[i];
            if(w.continueText(c))
            {
                string typed = w.getTyped();
                typing += typed + "\n";

                // Type Full Word
                if(typed.Equals(w.text.ToLower()))
                {
                    Debug.Log("Typed: " + w.text);
                    words.Remove(w);
                    break;
                }
            }
        }
        display.SetText(typing);
    }
}

[System.Serializable]
public class Word
{
    public string text;
    public UnityEvent onTyped;
    string hasTyped;
    int curChar;

    public Word(string t)
    {
        text = t.ToLower();
        hasTyped = "";
        curChar = 0;
    }

    public bool continueText(char c)
    {
        if (c.Equals(text[curChar]))
        {
            curChar++;
            hasTyped = text.Substring(0, curChar);

            if(curChar >= text.Length)
            {
                onTyped.Invoke();
                curChar = 0;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public string getTyped()
    {
        return hasTyped;
    }
}
