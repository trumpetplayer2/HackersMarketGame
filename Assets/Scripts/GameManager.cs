using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float money = 0;
    public int day = 0;
    public float[] rent = {100, 200, 500, 750, 1000, 1500, 2000, 2500, 4000, 5000};
    public GameObject InvestMenu;
    public GameObject HackMenu;
    public GameObject stockGUIMenu;
    public GameObject endDayObject;
    public EndDayMenu EndDay;
    public GameObject Pause;
    public GameObject Win;
    //Chosen by player at beginning of day
    public InvestManager investManager;
    public InvestManager investRubric;
    public TextMeshPro Balance;
    public TextMeshPro time;
    public TextMeshPro StockName;
    public InvestManager[] stockOptions;
    public StockOption[] stockMenuOptions;
    

    float gains = 0;
    float expenditure = 0;
    float currentRent = 0;
    float totalGains = 0;
    float totalExpenditure = 0;
    float totalScore = 0;
    int hour = 0;
    bool isPaused = false;

   


    // Start is called before the first frame update
    void Start()
    {
        //Initialize the scene
        instance = this;
        InvestMenu.SetActive(true);
        HackMenu.SetActive(false);
        //Do some stuff
        investManager = investRubric.CloneViaFakeSerialization();
        //Start Day 1
        newDay();
        endDayObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hour > 16 && !isPaused)
        {
            endDay();
        }
        int tempHour = hour + 6;
        if(tempHour > 12)
        {
            time.text = "Day " + day.ToString() + " " + (tempHour - 12) + ":00 PM";
        }
        else if (tempHour == 12)
        {
            time.text = "Day " + day.ToString() + " " + tempHour + ":00 PM";
        }
        else
        {
            time.text = "Day " + day.ToString() + " " + tempHour + ":00 AM";
        }
        if (InvestMenu.activeSelf)
        {
            investManager.investScreenUpdate();
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
        //Hide End Day if up
        endDayObject.SetActive(false);
        //Reset Earnings and Expenditures
        gains = 0;
        expenditure = 0;
        //Update the day value
        day += 1;
        //Reset hour
        hour = 0;
        //Update the new rent goal
        updateRent();
        //Generate 3 random Stocks to choose from
        for(int i = 0; i < stockMenuOptions.Length; i++)
        {
            InvestManager temp = investRubric.initializeNewManager(stockOptions[i]);
            temp.stockName = generateStockName();
            stockOptions[i] = temp;
        }
        //Insert Menu here later, for now we hardpick option 0
        DisplayStockOptions();
        updateBalance();
    }

    public void chooseStock(int possibility)
    {
        investManager = stockOptions[possibility];
        investManager.updateNodes();
        StockName.text = investManager.stockName;
        stockGUIMenu.SetActive(false);
        isPaused = false;
    }

    public void endDay()
    {
        isPaused = true;
        float score = 0;
        //Make sure on the invest screen, if not, switch
        stockMenu();
        //Cash out
        cashOut();
        //Remove Rent
        removeMoney(currentRent);
        //Calculate score
        score = (Mathf.Floor(day * 1.5f) * (gains - expenditure)) + money;
        //Update total scores
        totalScore += score;
        totalExpenditure += expenditure;
        totalGains += gains;
        //Update Displayed Score
        EndDay.UpdateDisplay(score, money, gains, expenditure, currentRent);
        //Check if broke after payment
        //Also check if Day 8 and should display "Win"
        //if (money < 10)
        //{
        //    //You lose
        //    lose();
        //}
        //else if (day == 7)
        //{
        //    //You win
        //    win();
        //}
        //else if (money < 10)
        //{
        //    //If you have perfect amount last day, you win
        //    //otherwise, you lose, as you can't make money
        //    //the next day
        //    lose();
        //}
        //else
        //{
            //Trigger New Day
            endDayObject.SetActive(true);
        //}
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

    public void stockMenu()
    {
        InvestMenu.SetActive(true);
        HackMenu.SetActive(false);
        //Update Stock Display
        investManager.updateNodes();
    }

    public void hackMenu()
    {
        InvestMenu.SetActive(false);
        HackMenu.SetActive(true);
        //Run a puzzle generator


    }

    public void invest(float amount)
    {
        //No investing nonexistant money
        if(money - amount < 0) { return; }
        //Update money and Invested
        removeMoney(amount);
        investManager.invest(amount);
        //Update Text
        Balance.text = "Rent: " + currentRent.ToString("c2") + " Invested: " + investManager.getAmountInvested().ToString("c2") + " Balance: " + money.ToString("c2");
        updateBalance();
        investManager.updateNodes();
    }

    public void cashOut()
    {
        addMoney(investManager.cashOut());
        updateBalance();
    }

    public void passTime(int hours)
    {
        if (isPaused) { return; }
        int temp = hour;
        hour += hours;
        //Update stocks
        for (int i = temp; i < hour; i++)
        {
            investManager.passHour();
        }
        updateBalance();
    }

    public void updateBalance()
    {
        Balance.text = "Invested: " + investManager.getAmountInvested().ToString("c2") + " Balance: " + money.ToString("c2");
    }

    public string generateStockName()
    {
        string temp = generatePrefix() + " " + generateSuffix();
        return temp;
    }

    public string generatePrefix()
    {
        string temp = "";
            int rng = Mathf.FloorToInt(Random.Range(1, 10));
        switch (rng)
        {
            case 1:
                temp = "Flying";
                break;
            case 2:
                temp = "Ye Olde";
                break;
            case 3:
                temp = "Deadly";
                break;
            case 4:
                temp = "Destructive";
                break;
            case 5:
                temp = "Glowing";
                break;
            case 6:
                temp = "Developing";
                break;
            case 7:
                temp = "Lost";
                break;
            case 8:
                temp = "Colorful";
                break;
            case 9:
                temp = "New";
                break;
            case 10:
                temp = "Favoring";
                break;
            default:
                temp = "Boring";
                break;
        }
        return temp;
    }

    public string generateSuffix()
    {
        string temp = "";
        int rng = Mathf.FloorToInt(Random.Range(1, 10));
        switch (rng)
        {
            case 1:
                temp = "Cats";
                break;
            case 2:
                temp = "Dogs";
                break;
            case 3:
                temp = "Tacos";
                break;
            case 4:
                temp = "Cause";
                break;
            case 5:
                temp = "Delays";
                break;
            case 6:
                temp = "Generators";
                break;
            case 7:
                temp = "Cars";
                break;
            case 8:
                temp = "Furniture";
                break;
            case 9:
                temp = "Software";
                break;
            case 10:
                temp = "Distractions";
                break;
            default:
                temp = "Cardboard Boxes";
                break;
        }
        return temp;
    }

    public void updateStockOptions()
    {
        for(int i = 0; i < stockMenuOptions.Length; i++)
        {
            stockMenuOptions[i].UpdateDisplay(stockOptions[i]);
        }
    }

    public void DisplayStockOptions()
    {
        updateStockOptions();
        stockGUIMenu.SetActive(true);
    }
}
