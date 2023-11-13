using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestManager : MonoBehaviour
{
    //These are current invest amounts along with history of previous points
    public float[] investPoints = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //These are objects that correspond with points. This will likely be swapped with a script handler
    public InvestNode[] investObjects;
    //This is vertical range around highest and lowest
    public float range = 5;

    //This contains current lowest
    float lowest = 0;
    //This contains current highest
    float highest = 0;
    
    public void setStock(int number, float price)
    {
        if (price < 0) price = 0;
        if(number > 9) number = 9;
        if(number < 0) number = 0;
        investPoints[number] = price;

        //Update highest/lowest as needed
        if (price > highest) highest = price + range;
        if (price < lowest) lowest = price - range;
        if (lowest < 0) lowest = 0;
        //Update objects
    }
    public void updateStock(float multiplier)
    {
        float temp = investPoints[investPoints.Length - 1] * multiplier;
        for(int i = 0; i<investPoints.Length - 1; i++)
        {
            investPoints[i] = investPoints[i + 1];
        }
        investPoints[investPoints.Length-1] = temp;
        //Update highest/lowest as needed
        if(temp > highest) highest = temp + range;
        if(temp < lowest) lowest = temp - range;
        if(lowest < 0) lowest = 0;
        //Update Objects

    }

    public void updateNodes()
    {

    }
    
}
