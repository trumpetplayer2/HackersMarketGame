using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestNode : MonoBehaviour
{
    //X Should never change, however X WILL, so we should plan for such
    public int maxY;
    public int minY;
    public void UpdateY(float value, float max, float min)
    {
        //Find where the value falls on a scale from Max to min
    }
}
