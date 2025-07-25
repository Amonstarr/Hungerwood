using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI hydrationCounter; // <- Ubah dari Text ke TextMeshProUGUI
    public GameObject playerState;
    private float currentHydration, maxHydration;

    void Awake()
    {
        slider = GetComponent<Slider>();   
    }

    void Update()
    {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydration;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydration;

        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;

        hydrationCounter.text = currentHydration + "%";
    }
}

