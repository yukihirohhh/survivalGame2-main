using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;
    public Text healthCounter;

    public GameObject playerState;

    private float currentHealth, maxHealth;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth; //0 - 1
        slider.value = fillValue;

        healthCounter.text = currentHealth + "/" + maxHealth; // 80/100


    }
}
