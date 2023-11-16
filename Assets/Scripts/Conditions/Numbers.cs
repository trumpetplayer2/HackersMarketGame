using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers : Condition
{
    int amount = 0;
    public void initialize(string correctAnswer)
    {
        int count = 0;
        foreach (char c in correctAnswer.ToCharArray())
        {
            if (char.IsNumber(c))
            {
                count += 1;
            }
        }
        amount = count;
    }
    public string getText()
    {
        return "There are " + amount + " numbers";
    }
    public bool requirement(string input)
    {
        int count = 0;
        foreach (char c in input.ToCharArray())
        {
            if (char.IsNumber(c))
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
        return 4;
    }
}
