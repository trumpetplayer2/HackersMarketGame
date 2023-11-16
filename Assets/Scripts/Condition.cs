using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Condition
{
    public int id();
    public void initialize(string correctAnswer);
    public string getText();
    public bool requirement(string choice);
}
