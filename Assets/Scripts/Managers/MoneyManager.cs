// Manages palyers money

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    // Public variables
    public Text moneyText;
    public Animator coinAnimator;
    public int moneyCount;
    public int winMoney = 0;
    public int servedCustmers = 0;
    public int clothPriceForPlayer = 50;
    public int clothPriceForCustomer = 75;
    public static int moneyInStart;

    private void Start()
    {
        // Get money amount from ScoresManager
        UpdateMoneyManagerMoneyCount();

        // Set money amount that was in the beginnig of the level
        moneyInStart = moneyCount;
    }

    // See how much money is left
    public int GetMoney()
    {
        return moneyCount;
    }

    // Remove money by the amount of clothing
    public void RemoveMoney()
    {
        // If there is money left
        if (moneyCount > 0)
        {
            // Remove money by the amount of clothing price
            moneyCount -= clothPriceForPlayer;
            ChangeMoneyText();
        }

        // Update ScoresManagers money amount
        UpdateMoneyScore();
    }

    // Remove spesific amount of money
    public void RemoveMoney(int money)
    {
        // If there is money left
        if (moneyCount > 0)
        {
            moneyCount -= money;
            ChangeMoneyText();
        }

        // Update ScoresManagers money amount
        UpdateMoneyScore();
    }

    // Add money by the amount of clothing
    public void AddMoney()
    {
        coinAnimator.SetTrigger("Spin");                            // Play coin spin animation
        moneyCount += clothPriceForCustomer;                        // Add money to total money amount
        winMoney += clothPriceForCustomer;                          // Add money to win money amount
        ChangeMoneyText();

        // Update ScoresManagers money amount
        UpdateMoneyScore();
    }

    // Add money by the certain amount
    public void AddMoney(int sum)
    {
        // Add money
        moneyCount += sum;
        ChangeMoneyText();

        // Update ScoresManagers money amount
        UpdateMoneyScore();
    }

    // Reset money count to what is was at the beginning of the level
    public void ResetMoneyAmount()
    {
        LifeTimerManager.lives--;
        moneyCount = moneyInStart;
        ChangeMoneyText();

        // Update ScoresManagers money amount
        UpdateMoneyScore();
    }

    // Change money text
    public void ChangeMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = moneyCount.ToString();
        }     
    }

    // Update ScoresManagers money amount
    public void UpdateMoneyScore()
    {
        ScoresManager.moneyAmount = moneyCount;
    }

    // Get ScoresManagers money amount
    public void UpdateMoneyManagerMoneyCount()
    {
        moneyCount = ScoresManager.moneyAmount;
        ChangeMoneyText();
    }

    // Raise profit margin when player buys IoT devices
    public void RaiseProfitMargin(int percent)
    {
        Debug.Log("Vanha hinta: " + clothPriceForCustomer);

        float newPrice = (float)clothPriceForCustomer / 100 * percent;  // Calculate how much price will grow
        clothPriceForCustomer += (int)newPrice;                         // Add to the old price

        Debug.Log("Uusi hinta: " + clothPriceForCustomer);
        Debug.Log("Hinnan nousu: " + newPrice);
    }
}