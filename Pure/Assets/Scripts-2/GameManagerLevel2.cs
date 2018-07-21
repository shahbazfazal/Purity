using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLevel2 : MonoBehaviour
{
    public float FL_Money;
    public float FL_Health;
    public float PurityLevel;
    public float MethAmount;
    public float PricePerGram;
    public float PriceOfEach;

    public Text mainText;
    public Slider GramsSlider;
    public Text SliderOutput;
    public Text HealthBarText;
    public Text MoneyText;

    public GameObject SellSlider;
    public GameObject SellButton;
    public GameObject CanvasMakeMore;

    void Start()
    {
        //get purty level from level 1
        PurityLevel = PlayerPrefs.GetInt("FinalPurityLevel");
        //get health after previous sell
        FL_Health = PlayerPrefs.GetFloat("Health");
        //get money after previous sell
        FL_Money = PlayerPrefs.GetFloat("Money");

        MethAmount = 35;
        PricePerGram = 80;
         
        PriceCalculation();
        SellMeth();

    }

    void Update()
    {
        //slider number
        SliderOutput.text = GramsSlider.value.ToString();

        //health text
        HealthBarText.text = "Health: " + 
            PlayerPrefs.GetFloat("Health", 100);

        //money text
        MoneyText.text = "Money: $" + 
            PlayerPrefs.GetFloat("Money", 0);

        //reset values

        if (Input.GetKeyDown (KeyCode.Space))
        {
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("Money");
            FL_Health = 100;
            FL_Money = 0;
        }
        if (MethAmount <= 0)
        {
            //display button to go back to level 1
            SellSlider.SetActive(false);
            SellButton.SetActive(false);
            CanvasMakeMore.SetActive(true);
        }
    }

    public void StartLevel1()
    {
        Application.LoadLevel("Level1");
    }

    void PriceCalculation()
    {
        // 1 * 80 * PurityLevel
        PriceOfEach = (MethAmount / MethAmount) *
            (PricePerGram * PurityLevel);
    }

    void SellMeth()
    {
        mainText.text = "Methamphetamine sells for\n" +
            "$80 per gram multiplied\n" +
            "by the level of purity.\n\n" +
            "You have " + MethAmount + "g prepared\n" +
            "at the purity level of " + PurityLevel + ".\n\n" +
            "Your price is $" + PriceOfEach + " per gram.\n\n" +
            "The more you sell, the higher the risk of getting caught is!\n" +
            "How many g's do you want\n" +
            "to sell?";

        GramsSlider.maxValue = MethAmount;
    }

    public void ReduceHealth()
    {
        FL_Health -= GramsSlider.value;
        FL_Money += (PriceOfEach * GramsSlider.value);
        PlayerPrefs.SetFloat("Health", FL_Health);
        PlayerPrefs.SetFloat("Money", FL_Money);

        Debug.Log(FL_Health);

        if (FL_Health <= 0)
        {
            FL_Health = 0;
            Debug.Log("NIGGA DIDED");
        }

        
        MethAmount -= GramsSlider.value;
        SellMeth();
    }
}