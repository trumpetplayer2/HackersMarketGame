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
    public float range = 0.5f;
    //Stock Line
    public LineRenderer line;

    //This contains current lowest
    float lowest = 0;
    //This contains current highest
    float highest = 0;

    private void Update()
    {
        //TODO: Add a check for which screen should be shown
        investScreenUpdate();
        
    }

    private void investScreenUpdate()
    {
        if (line != null)
        {
            line.positionCount = investObjects.Length;
            for (int i = 0; i < investObjects.Length; i++)
            {
                if (investObjects[i] != null)
                {
                    line.SetPosition(i, investObjects[i].transform.position);
                }
            }
        }
    }

    public void testSet(float price)
    {
        setStock(investObjects.Length - 1, price);
    }

    public void setStock(int number, float price)
    {
        if (price < 0) price = 0;
        if(number > 9) number = 9;
        if(number < 0) number = 0;
        investPoints[number] = price;

        //Update highest/lowest as needed
        if (price > highest) highest = price;
        if (price < lowest) lowest = price;
        if (lowest < 0) lowest = 0;
        //Update objects
        updateNodes();
    }
    public void updateStock(float multiplier)
    {
        float temp = investPoints[investPoints.Length - 1] * multiplier;
        if(temp < 0) temp = 0;
        lowest = investPoints[1];
        highest = investPoints[1];
        for(int i = 0; i<investPoints.Length - 1; i++)
        {
            investPoints[i] = investPoints[i + 1];
            if (investPoints[i] < lowest) lowest = investPoints[i];
            if (investPoints[i] > highest) highest = investPoints[i];
        }
        investPoints[investPoints.Length-1] = temp;
        //Update highest/lowest as needed
        if(temp > highest) highest = temp;
        if(temp < lowest) lowest = temp;
        //Update Objects
        updateNodes();
    }

    public void updateNodes()
    {
        for(int i = 0; i<investObjects.Length; i++)
        {
            investObjects[i].UpdateY(investPoints[i], highest, lowest);
        }
    }
    
}
