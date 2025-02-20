using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{
    private Slider slider;
    public Text CaloriesCounter;

    public GameObject playerState;

    private float currentCalories, maxCalories;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        float fillValue = currentCalories / maxCalories; //0 - 1
        slider.value = fillValue;

        CaloriesCounter.text = currentCalories + "/" + maxCalories;


    }
}
