using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odd : Condition
{
    int amount = 0;
    public void initialize(string correctAnswer)
    {
        int count = 0;
        foreach (char c in correctAnswer.ToCharArray())
        {
            if (c == '1' || c == '3' || c == '5' || c == '7' || c == '9')
            {
                count += 1;
            }
        }
        amount = count;
    }
    public string getText()
    {
        return "There are " + amount + " odd numbers";
    }
    public bool requirement(string input)
    {
        int count = 0;
        foreach(char c in input.ToCharArray())
        {
            if(c == '1' || c == '3' || c == '5' || c == '7' || c == '9')
            {
                count += 1;
            }
        }
        if(count == amount)
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
        return 0;
    }
}
