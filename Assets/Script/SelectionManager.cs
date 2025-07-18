using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public float interactionDistance = 8f;
    public Transform player;

    TextMeshProUGUI interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable)
            {
                float distance = Vector3.Distance(player.position, selectionTransform.position);

                if (distance <= interactionDistance)
                {
                    interaction_text.text = interactable.GetItemName();
                    interaction_Info_UI.SetActive(true);

                    if (selectionTransform.CompareTag("Item") && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (InventorySystem.Instance.CheckIfFull())
                        {
                            Debug.Log("Inventory is Full");
                        }
                        else
                        {
                            InventorySystem.Instance.AddToInventory(interactable.ItemName);
                            Destroy(interactable.gameObject);
                            interaction_Info_UI.SetActive(false);
                        }
                    }
                }
                else
                {
                    interaction_Info_UI.SetActive(false);
                }
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}
