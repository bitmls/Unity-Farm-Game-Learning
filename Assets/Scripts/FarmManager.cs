using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectedPlant;
    public bool isPlanting = false;

    public int money = 100;
    public TextMeshProUGUI moneyText;

    public Color buyColor;
    public Color cancelColor;

    public bool isToolSelecting = false;
    // 1- water 2- Fertilizer 3- Buy Plot
    public int selectedTool = 0;

    void Start()
    {
        moneyText.text = "$" + money;
    }

    public void SelectPlant(PlantItem newPlant)
    {
        if (selectedPlant == newPlant)
        {
            selectedPlant.buttonImage.color = buyColor;
            selectedPlant.buttonText.text = "CHOOSE";

            selectedPlant = null;
            isPlanting = false;
        }
        else
        {
            if (selectedPlant != null)
            {
                selectedPlant.buttonImage.color = buyColor;
                selectedPlant.buttonText.text = "CHOOSE";
            }

            selectedPlant = newPlant;
            isPlanting = true;

            selectedPlant.buttonImage.color = cancelColor;
            selectedPlant.buttonText.text = "CANCEL";
        }
    }

    public void Transaction(int value)
    {
        money += value;
        moneyText.text = "$" + money;
    }

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {

        }
        else
        {
            CheckSelection();
        }
    }

    private void CheckSelection()
    {
        if(isPlanting)
        {
            isPlanting = false;
            if (selectedPlant != null)
            {
                selectedPlant.buttonImage.color = buyColor;
                selectedPlant.buttonText.text = "CHOOSE";
                selectedPlant = null;
            }
        }
        if(isToolSelecting)
        {

        }
    }
}
