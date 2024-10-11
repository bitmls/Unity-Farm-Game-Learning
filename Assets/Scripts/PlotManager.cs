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

    private bool isDry = true;
    public Sprite drySprite;
    public Sprite wateredSprite;

    private float growthSpeed = 1f;

    public bool isBought = true;
    public Sprite unavailableSprite;

    private FarmManager farmManager;

    void Start()
    {
        farmManager = transform.parent.GetComponent<FarmManager>();

        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        plot = GetComponent<SpriteRenderer>();
        if (!isBought)
        {
            plot.sprite = unavailableSprite;
        }
        else
        {
            plot.sprite = drySprite;
        }
    }

    void Update()
    {
        UpdatePlantGrowth();
    }

    private void UpdatePlantGrowth()
    {
        if (isPlanted && !isDry)
        {
            growthTimer -= growthSpeed * Time.deltaTime;

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
            if (plantStage == selectedPlant.plantStages.Length - 1 && !farmManager.isPlanting && !farmManager.isToolSelecting)
            {
                Harvest();
            }
        }
        else if (farmManager.isPlanting && farmManager.money >= farmManager.selectedPlant.plant.buyPrice && isBought)
        {
            Plant(farmManager.selectedPlant.plant);
        }
        if (farmManager.isToolSelecting)
        {
            switch (farmManager.selectedTool)
            {
                case 1:
                    if (isBought)
                    {
                        isDry = false;
                        plot.sprite = wateredSprite;
                        if (isPlanted)
                            ChangePlantStage();
                    }
                    break;
                case 2:
                    if (farmManager.money >= 10 && isBought)
                    {
                        farmManager.Transaction(-10);
                        growthSpeed = 1.5f;
                    }
                    break;
                case 3:
                    if (farmManager.money >= 100 && !isBought)
                    {
                        farmManager.Transaction(-100);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMouseOver()
    {
        if (farmManager.isPlanting)
        {
            // can't plant
            if (isPlanted || farmManager.money < farmManager.selectedPlant.plant.buyPrice || !isBought)
            {
                plot.color = unavailableColor;
            }
            // can plant
            else
            {
                plot.color = availableColor;
            }
        }
        if (farmManager.isToolSelecting)
        {
            switch (farmManager.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && farmManager.money >= (farmManager.selectedTool - 1) * 10)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && farmManager.money >= 100)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
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

        isDry = true;
        plot.sprite = drySprite;

        growthSpeed = 1f;
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
        if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
