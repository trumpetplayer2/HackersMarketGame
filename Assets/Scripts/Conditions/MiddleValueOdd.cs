using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleValueOdd : Condition
{
    bool oddMiddle = false;
    public void initialize(string correctAnswer)
    {
        char c = correctAnswer.ToCharArray()[2];
        oddMiddle = (c == '1' || c == '3' || c == '5' || c == '7' || c == '9');
    }
    public string getText()
    {
        string temp = "The middle is ";
        if (oddMiddle)
        {
            temp += "an odd number.";
        }
        else
        {
            temp += "not an odd number.";
        }
        return temp;
    }
    public bool requirement(string input)
    {
        char c = input.ToCharArray()[2];
        bool temp = (c == '1' || c == '3' || c == '5' || c == '7' || c == '9');
        return (temp == oddMiddle);
    }
    public int id()
    {
        return 6;
    }
}
