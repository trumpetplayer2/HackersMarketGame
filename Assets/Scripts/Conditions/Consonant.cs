using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consonant : Condition
{
    int amount = 0;
    public void initialize(string correctAnswer)
    {
        int count = 0;
        foreach (char c in correctAnswer.ToCharArray())
        {
            if (!(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y') && char.IsLetter(c))
            {
                count += 1;
            }
        }
        amount = count;
    }
    public string getText()
    {
        return "There are " + amount + " consonant";
    }
    public bool requirement(string input)
    {
        int count = 0;
        foreach (char c in input.ToCharArray())
        {
            if (!(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y') && char.IsLetter(c))
            {
                count += 1;
            }
        }
        if (count == amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int id()
    {
        return 3;
    }
}
