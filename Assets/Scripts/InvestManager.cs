using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestManager : MonoBehaviour
{
    public string stockName = "";
    //These are current invest amounts along with history of previous points
    public float[] investPoints = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //These are objects that correspond with points. This will likely be swapped with a script handler
    public InvestNode[] investObjects;
    //This is vertical range around highest and lowest
    public float range = 0.5f;
    //Variation
    public float variation = 0.2f;
    //Stock Line
    public LineRenderer line;
    //Amount invested
    public float amountInvested = 0;
    //Skew if existant
    public float skew = 0;


    //This contains current lowest
    public float lowest = 0;
    //This contains current highest
    public float highest = 1;

    public InvestManager initializeNewManager(InvestManager clone)
    {
        //Generate a random variation
        clone.variation = Random.Range(0.2f, 0.75f);
        //Generate a random base stock price
        float startPrice = Random.Range(100, 500);
        //Generate 10 random invest points using a base value and a multiplier within range
        clone.investPoints[0] = startPrice;
        clone.lowest = startPrice;
        for(int i = 1; i < investPoints.Length; i++)
        {
            float temp = clone.investPoints[i-1] * Random.Range(1 - clone.variation, 1 + clone.variation);
            clone.investPoints[i] = temp;
            if (clone.highest < temp) clone.highest = temp;
            if (clone.lowest > temp) clone.lowest = temp;
        }
        for(int i = 0; i < investObjects.Length; i++)
        {
            clone.investObjects[i] = investObjects[i];
        }
        clone.range = range;
        clone.line = line;

        return clone;
    }


    public void investScreenUpdate()
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
        //Update amount invested
        amountInvested = amountInvested * multiplier;
        //Update Nodes
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
        if (investPoints[investPoints.Length - 1] > highest) highest = investPoints[investPoints.Length - 1];
        for (int i = 0; i<investObjects.Length; i++)
        {
            investObjects[i].UpdateY(investPoints[i], highest, lowest);
        }
    }
    
    public float getAmountInvested()
    {
        return amountInvested;
    }

    public void invest(float amount)
    {
        amountInvested += amount;
        investPoints[investPoints.Length - 1] += amount;
    }

    public float cashOut()
    {
        float temp = amountInvested;
        amountInvested = 0;
        investPoints[investPoints.Length - 1] -= temp;
        updateNodes();
        return temp;
    }

    public void passHour()
    {
        float multiplier = Random.Range(1 - variation, 1 + variation);
        multiplier += skew;
        updateStock(multiplier);
    }
}
