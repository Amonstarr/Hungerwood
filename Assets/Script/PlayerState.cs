using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    //Player Health
    public float currentHealth;
    public float maxHealth;


    //Player Calories
    public float currentCalories;
    public float maxCalories;
    float distanceTravelled = 0;
    Vector3 lastPosition;
    [SerializeField] private GameObject playerBody;

    //Player Hydration
    public float currentHydration;
    public float maxHydration;

    public bool isHydrationActive;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {
            currentHydration -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentCalories -= 1;
        }
    }
}
