using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreable : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public TextMeshPro[] tmproTexts;
    public void updateTexts(string[] text)
    {
        int iterations = 0;
        if(text.Length <= texts.Length)
        {
            iterations = text.Length;
        }
        else
        {
            iterations = texts.Length;
        }
        for(int i = 0; i < iterations; i++)
        {
            texts[i].text = text[i];
        }
    }

    public void updateTMP(string[] text)
    {
        int iterations = 0;
        if (text.Length <= tmproTexts.Length)
        {
            iterations = text.Length;
        }
        else
        {
            iterations = texts.Length;
        }
        for (int i = 0; i < iterations; i++)
        {
            tmproTexts[i].text = text[i];
        }
    }
}
