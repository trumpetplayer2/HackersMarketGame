using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StockOption : MonoBehaviour
{
    public TextMeshProUGUI StockName;
    public TextMeshProUGUI StockRange;
    
    public void UpdateDisplay(InvestManager stock)
    {
        StockName.text = stock.stockName;
        StockRange.text = (1 - stock.variation).ToString("0.00") + "-" + (1 + stock.variation).ToString("0.00");
    }
}
