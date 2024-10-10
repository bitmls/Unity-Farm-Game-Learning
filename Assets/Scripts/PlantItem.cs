using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Image icon;

    public Image buttonImage;
    public TextMeshProUGUI buttonText;

    private FarmManager farmManager;

    void Start()
    {
        farmManager = FindAnyObjectByType<FarmManager>();

        InitializeUI();
    }

    private void InitializeUI()
    {
        nameText.text = plant.plantName;
        priceText.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }

    public void BuyPlant()
    {
        farmManager.SelectPlant(this);
    }
}
