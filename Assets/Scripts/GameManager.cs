using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public float money = 0;
    public int day = 0;
    public float[] rent = {100, 200, 500, 750, 1000, 1500, 2000, 2500, 4000, 5000};
    public Camera investCam;

    float gains = 0;
    float expenditure = 0;
    float currentRent = 0;
    float totalGains = 0;
    float totalExpenditure = 0;
    float totalScore = 0;
    int hour = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        //Initialize the scene

        //Do some stuff

        //Start Day 1
        newDay();
    }

    // Update is called once per frame
    void Update()
    {
        if(hour > 16)
        {
            endDay();
        }
    }

    /// <summary>
    /// This shouldn't be used outside of this function.
    /// It is here in case I find a use for it elsewhere later
    /// It also allows rounding to cents a lot easier when setting the money value
    /// </summary>
    /// <param name="temp"></param>
    private void setMoney(float temp)
    {
        temp = Mathf.Ceil(temp * 100) / 100;
        money = temp;
    }

    /// <summary>
    /// Adds money to money and gains values
    /// </summary>
    /// <param name="earnings"></param>
    public void addMoney(float earnings)
    {
        earnings = Mathf.Ceil(earnings * 100)/100;
        money += earnings;
        gains += earnings;
    }
    /// <summary>
    /// Subtracts spendins from money, and adds spendings to total expenditure.
    /// This will be used for a report at the end of the day
    /// </summary>
    /// <param name="spendings"></param>
    public void removeMoney(float spendings)
    {
        spendings = Mathf.Ceil(spendings * 100) / 100;
        money -= spendings;
        expenditure += spendings;
    }

    public void newDay()
    {
        //Reset Earnings and Expenditures

        //Update the day value
        day += 1;
        //Reset hour
        hour = 0;
        //Update the new rent goal
        updateRent();
    }

    public void endDay()
    {
        float score = 0;
        //Make sure on the invest screen, if not, switch
        
        //Cash out

        //Calculate score
        score = (Mathf.Floor(day * 1.5f) * (gains - expenditure)) + money;
        //Display Score

        //Update total scores
        totalScore += score;
        totalExpenditure += expenditure;
        totalGains += gains;
        //Check if broke after payment
        //Also check if Day 8 and should display "Win"
        if (money < 0)
        {
            //You lose
            lose();
        }
        else if (day == 7)
        {
            //You win
            win();
        }
        else if (money == 0)
        {
            //If you have perfect amount last day, you win
            //otherwise, you lose, as you can't make money
            //the next day
            lose();
        }
        else
        {
            //Trigger New Day
            newDay();
        }
    }

    private void updateRent()
    {
        float temp;
        if(day < rent.Length)
        {
            temp = rent[day - 1];
        }
        else
        {
            temp = Mathf.Floor(rent[rent.Length - 1] * (1.5f * (day - rent.Length)));
        }
        currentRent = temp;
    }

    void lose()
    {
        //Show lose screen

        //Set total score

    }

    void win()
    {
        //Show win screen

        //Set total score

        //Show endless button
        if(money > 0)
        {
            //If money > 0, allow it to be clicked

        }
    }

    public void endless()
    {
        //Hide Win Screen

        //Trigger new day
        newDay();
    }
}
