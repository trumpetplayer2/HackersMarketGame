using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndDayMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gainsText;
    public TextMeshProUGUI expenditureText;
    public TextMeshProUGUI rentText;

    public void UpdateDisplay(float score, float money, float gains, float expenditure, float rent)
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
        moneyText.text = "Remaining: " + money.ToString("c2");
        gainsText.text = "Profit: + " + gains.ToString("c2");
        expenditureText.text = "Spendings: - " + expenditure.ToString("c2");
        rentText.text = "Rent: " + rent.ToString("c2");
    }
}
