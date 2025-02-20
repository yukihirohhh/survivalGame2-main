using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public Text HydrationCounter;

    public GameObject playerState;

    private float currentHydration, maxHydration;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        currentHydration = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydrationPercent;

        float fillValue = currentHydration / maxHydration; //0 - 1
        slider.value = fillValue;

        HydrationCounter.text = currentHydration + "/" + maxHydration;


    }
}
