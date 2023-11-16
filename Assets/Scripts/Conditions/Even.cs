using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Even : Condition
{
    int amount = 0;
    public void initialize(string correctAnswer)
    {
        int count = 0;
        foreach (char c in correctAnswer.ToCharArray())
        {
            if (c == '2' || c == '4' || c == '6' || c == '8' || c == '0')
            {
                count += 1;
            }
        }
        amount = count;

    }
    public string getText()
    {
        return "There are " + amount + " even numbers";
    }
    public bool requirement(string input)
    {
        int count = 0;
        foreach (char c in input.ToCharArray())
        {
            if (c == '2' || c == '4' || c == '6' || c == '8' || c == '0')
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
        return 1;
    }

}
