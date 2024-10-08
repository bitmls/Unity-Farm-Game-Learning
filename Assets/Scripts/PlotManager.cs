using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    private bool isPlanted = false;

    public SpriteRenderer plant;
    public Sprite[] plantStages;
    private int plantStage = 0;

    [SerializeField]
    private float growthDuration = 2.0f;
    private float growthTimer = 0;

    void Start()
    {

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

            if (growthTimer < 0 && plantStage < plantStages.Length - 1)
            {
                growthTimer = growthDuration;
                plantStage++;
                ChangePlantStage();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1)
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
        growthTimer = growthDuration;
        plant.gameObject.SetActive(true);
    }

    private void ChangePlantStage()
    {
        plant.sprite = plantStages[plantStage];
    }
}
