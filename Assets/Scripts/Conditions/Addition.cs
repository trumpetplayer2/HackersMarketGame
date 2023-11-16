using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addition : Condition
{
    int amount = 0;
    public void initialize(string correctAnswer)
    {
        int count = 0;
        foreach (char c in correctAnswer.ToCharArray())
        {
            if (char.IsNumber(c))
            {
                count += int.Parse(c.ToString());
            }
        }
        amount = count;
    }
    public string getText()
    {
        return "All numbers added together equal " + amount;
    }
    public bool requirement(string input)
    {
        int count = 0;
        foreach (char c in input.ToCharArray())
        {
            if (char.IsNumber(c))
            {
                count += int.Parse(c.ToString());
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
        return 0;
    }
}
