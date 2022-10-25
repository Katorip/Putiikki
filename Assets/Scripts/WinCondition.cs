using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    // Public variables
    [Header("Game Time Settings")]
    public int countdownTime = 65;
    public Text timeText;

    [Header("Managers")]
    public MoneyManager moneyManager;
    public SoundManager soundManager;

    [Header("Game Objects")]
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public GameObject winConditionMenu;
    public GameObject backgroundImage;
    public GameObject exitButton;
    public GameObject moneySprite;
    public GameObject customersSprite;
    public GameObject satisfactionSprite;
    public GameObject winMenuMoney;
    public GameObject winMenuCustomers;
    public GameObject winMenuSatisfaction;

    [Header("Text Objects")]
    public Text gameOverConditionText;
    public Text salesText;
    public Text bonusText;
    public Text totalText;
    public Text winMoneyText;
    public Text winCustomersText;
    public Text winSatisfactionText;
    public Text winMenuMoneyText;
    public Text winMenuCustomersText;
    public Text winMenuSatisfactionText;

    [Header("Misc")]
    public AudioSource gameEndSound;
    public float currentSatisfaction = 0.0f;
    public bool tutorial;

    // Win conditions
    [Header("Win Conditions")]
    public int moneyTarget;
    public int customerTarget;
    public float satisfactionTarget;
    public bool earnMoney;
    public bool serveCustomers;
    public bool getHappyCustomers;

    // Current conditions
    [Header("Current Conditions")]
    public float currentHappyCustomersCount = 0;

    // Private variables
    private bool condition;
    private bool alreadyLiveTaked;
    private int bonus;

    void Start()
    {
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        backgroundImage.SetActive(false);
        exitButton.SetActive(true);
        gameOverConditionText.text = "";
        alreadyLiveTaked = false;
        moneySprite.SetActive(false);
        customersSprite.SetActive(false);
        satisfactionSprite.SetActive(false);
        winConditionMenu.SetActive(false);
        winMenuMoney.SetActive(false);
        winMenuCustomers.SetActive(false);
        winMenuSatisfaction.SetActive(false);

        winMoneyText.color = Color.red;
        winCustomersText.color = Color.red;
        winSatisfactionText.color = Color.red;

        // Start game time
        ShowConditionInfo();
        UpdateConditionInfo();
        UpdateTimeText();

        if (!tutorial)
        {
            Time.timeScale = 0;
            SetWinConditionMenu();
        }    
    }

    public void SetWinConditionMenu()
    {
        winConditionMenu.SetActive(true);

        // If win condition is earning spesific amount of money
        if (earnMoney)
        {
            winMenuMoney.SetActive(true);
            winMenuMoneyText.text = moneyTarget.ToString();
        }

        // If win condition is serving spesific amount of customers
        if (serveCustomers)
        {
            winMenuCustomers.SetActive(true);
            winMenuCustomersText.text = customerTarget.ToString();
        }

        // If win condition is to get good customer satisfaction points
        if (getHappyCustomers)
        {
            winMenuSatisfaction.SetActive(true);
            winMenuSatisfactionText.text = satisfactionTarget.ToString();
        }
    }

    public void StartGameTime()
    {
        StartCoroutine(CountdownToStart());
        Time.timeScale = 1;

        if (winConditionMenu != null)
        {
            Destroy(winConditionMenu);
        }
    }

    // Start game time
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            UpdateTimeText();
            yield return new WaitForSeconds(1.0f);
            countdownTime--;
        }

        // Game is not paused
        Time.timeScale = 0;

        // Stop playing background music
        soundManager.StopBackgroundMusic();

        // Play game end sound
        if (SettingsManager.soundOn)
        {
            gameEndSound.Play();
        }

        // If level was a tutorial
        if (tutorial)
        {
            BeforeTutorialManager.tutorialPlayed = true;
        }

        // If win condition is earning spesific amount of money
        if (earnMoney)
        {
            EarnMoneyCondition();
        }

        // If win condition is serving spesific amount of customers
        if (serveCustomers)
        {
            ServeCustomersCondition();
        }

        // If win condition is to get good customer satisfaction points
        if (getHappyCustomers)
        {
            GetHappyCustomersCondition();
        }
    }

    // Update game time UI
    private void UpdateTimeText()
    {
        timeText.text = "Time left: " + countdownTime;
    }

    // Earn money win condition
    public void EarnMoneyCondition()
    {
        if (moneyManager.winMoney < moneyTarget)
        {
            gameOverMenu.SetActive(true);
            backgroundImage.SetActive(true);
            gameOverConditionText.text += "\n" + moneyManager.winMoney + "/" + moneyTarget + " money earned";
            GameOverCondition();
        }
        else
        {
            winMenu.SetActive(true);
            UpdateWinConditionText();
        }
    }

    // Serve customers win condition
    public void ServeCustomersCondition()
    {
        if (moneyManager.servedCustmers < customerTarget)
        {
            gameOverMenu.SetActive(true);
            backgroundImage.SetActive(true);
            gameOverConditionText.text += "\n" + moneyManager.servedCustmers + "/" + customerTarget + " customers served";
            GameOverCondition();
        }
        else
        {
            winMenu.SetActive(true);
            UpdateWinConditionText();
        }
    }

    // Get happy customers win condition
    public void GetHappyCustomersCondition()
    {
        if (currentHappyCustomersCount < satisfactionTarget)
        {
            gameOverMenu.SetActive(true);
            backgroundImage.SetActive(true);
            gameOverConditionText.text += "\n" + currentHappyCustomersCount + "/" + satisfactionTarget + " customer satisfaction";
            GameOverCondition();
        }
        else
        {
            winMenu.SetActive(true);
            UpdateWinConditionText();
        }
    }

    // Update winning UI
    private void UpdateWinConditionText()
    {
        bonus = (int)currentSatisfaction / 100 * 10;
        moneyManager.AddMoney(bonus);

        salesText.text = moneyManager.winMoney.ToString();
        bonusText.text = bonus.ToString();
        totalText.text = (moneyManager.winMoney + bonus).ToString();

        // Disable exit button
        exitButton.SetActive(false);
    }

    public void DoubleMoneyVideo()
    {
        moneyManager.winMoney = moneyManager.winMoney * 2;
        bonus = bonus * 2;

        moneyManager.AddMoney(moneyManager.winMoney + bonus);

        salesText.text = moneyManager.winMoney.ToString();
        bonusText.text = bonus.ToString();
        totalText.text = (moneyManager.winMoney + bonus).ToString();
    }

    private void GameOverCondition()
    {
        if (LifeTimerManager.lives > 0 && !alreadyLiveTaked)
        {
            LifeTimerManager.lives--;
            PlayerPrefs.SetString("_timer", "Standby");
            alreadyLiveTaked = true;
            exitButton.SetActive(false);
        }       
    }

    // Show win condition info based on what is needed for winning
    private void ShowConditionInfo()
    {
        // If win condition is earning spesific amount of money
        if (earnMoney)
        {
            moneySprite.SetActive(true);
        }

        // If win condition is serving spesific amount of customers
        if (serveCustomers)
        {
            customersSprite.SetActive(true);
        }

        // If win condition is to get good customer satisfaction points
        if (getHappyCustomers)
        {
            satisfactionSprite.SetActive(true);
        }
    }

    // Update win condition info
    public void UpdateConditionInfo()
    {
        // If win condition is earning spesific amount of money
        if (earnMoney)
        {
            winMoneyText.text = moneyManager.winMoney.ToString();

            if (moneyManager.winMoney >= moneyTarget)
            {
                winMoneyText.color = Color.green;
            }                   
        }

        // If win condition is serving spesific amount of customers
        if (serveCustomers)
        {
            winCustomersText.text = moneyManager.servedCustmers.ToString();

            if (moneyManager.servedCustmers >= customerTarget)
            {
                winCustomersText.color = Color.green;
            }           
        }

        // If win condition is to get good customer satisfaction points
        if (getHappyCustomers)
        {
            winSatisfactionText.text = currentSatisfaction.ToString("0");

            if (currentSatisfaction >= satisfactionTarget)
            {
                winSatisfactionText.color = Color.green;
            }          
        }
    }
}