using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaloriesBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI caloriesCounter; // <- Ubah dari Text ke TextMeshProUGUI
    public GameObject playerState;
    private float currentCalories, maxCalories;

    void Awake()
    {
        slider = GetComponent<Slider>();   
    }

    void Update()
    {
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;

        caloriesCounter.text = currentCalories + "/" + maxCalories;
    }
}
