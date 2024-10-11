using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public Image[] buttonImages;
    public Sprite normalButton;
    public Sprite selectedButton;

    void Start()
    {
        moneyText.text = "$" + money;
    }

    public void SelectPlant(PlantItem newPlant)
    {
        if (selectedPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();

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
            CheckSelection();
        }
        else
        {
            CheckSelection();
            isToolSelecting = true;
            selectedTool = toolNumber;
            buttonImages[selectedTool - 1].sprite = selectedButton;
        }
    }

    private void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if (selectedPlant != null)
            {
                selectedPlant.buttonImage.color = buyColor;
                selectedPlant.buttonText.text = "CHOOSE";
                selectedPlant = null;
            }
        }
        if (isToolSelecting)
        {
            if (selectedTool > 0)
            {
                buttonImages[selectedTool - 1].sprite = normalButton;
            }
            isToolSelecting = false;
            selectedTool = 0;
        }
    }
}
