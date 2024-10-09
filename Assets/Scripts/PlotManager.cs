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

    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
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
            if (plantStage == selectedPlant.plantStages.Length - 1)
            {
                Harvest();
            }
        }
        else
        {
            Plant();
        }
    }

    private void Harvest()
    {
        Debug.Log("Harvest");
        isPlanted = false;
        plant.gameObject.SetActive(false);
    }

    private void Plant()
    {
        Debug.Log("Plant");
        isPlanted = true;
        plantStage = 0;
        ChangePlantStage();
        growthTimer = selectedPlant.growthDuration;
        plant.gameObject.SetActive(true);
    }

    private void ChangePlantStage()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
