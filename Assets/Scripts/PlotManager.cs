using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    private bool isPlanted = false;

    private SpriteRenderer plant;
    private BoxCollider2D plantCollider;

    private int plantStage = 0;

    private float growthTimer = 0;

    public PlantObject selectedPlant;


    private SpriteRenderer plot;
    public Color availableColor;
    public Color unavailableColor;


    private FarmManager farmManager;

    void Start()
    {
        farmManager = transform.parent.GetComponent<FarmManager>();

        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        plot = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdatePlantGrowth();
    }

    private void UpdatePlantGrowth()
    {
        if (isPlanted)
        {
            growthTimer -= Time.deltaTime;

            if (growthTimer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                growthTimer = selectedPlant.growthDuration;
                plantStage++;
                ChangePlantStage();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !farmManager.isPlanting)
            {
                Harvest();
            }
        }
        else if (farmManager.isPlanting && farmManager.money >= farmManager.selectedPlant.plant.buyPrice)
        {
            Plant(farmManager.selectedPlant.plant);
        }
    }

    private void OnMouseOver()
    {
        if (farmManager.isPlanting)
        {
            // can't plant
            if (isPlanted || farmManager.money < farmManager.selectedPlant.plant.buyPrice)
            {
                plot.color = unavailableColor;
            }
            // can plant
            else
            {
                plot.color = availableColor;
            }
        }
    }

    private void OnMouseExit()
    {
        plot.color = Color.white;
    }

    private void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        farmManager.Transaction(selectedPlant.sellPrice);
    }

    private void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        farmManager.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        ChangePlantStage();
        growthTimer = newPlant.growthDuration;
        plant.gameObject.SetActive(true);
    }

    private void ChangePlantStage()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
