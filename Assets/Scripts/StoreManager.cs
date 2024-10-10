using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;

    private List<PlantObject> plantObjects = new List<PlantObject>();

    private void Awake()
    {
        // Assets/Resources/Plants
        var loadPlants = Resources.LoadAll("Plants", typeof(PlantObject));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((PlantObject)plant);
        }
        plantObjects.Sort(SortByPrice);
        foreach (var plant in plantObjects)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
        }
    }

    private int SortByPrice(PlantObject p1, PlantObject p2)
    {
        return p1.buyPrice.CompareTo(p2.buyPrice);
    }
    private int SortByGrowthDuration(PlantObject p1, PlantObject p2)
    {
        return p1.growthDuration.CompareTo(p2.growthDuration);
    }
}
