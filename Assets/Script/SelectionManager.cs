using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public float interactionDistance = 3f;
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
                    // Tampilkan nama objek di UI
                    interaction_text.text = interactable.GetItemName();
                    interaction_Info_UI.SetActive(true);

                    // Jika objek bertag "Item", bisa diambil saat klik
                    if (selectionTransform.CompareTag("Item") && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Debug.Log($"{interactable.GetItemName()} picked up!");
                        Destroy(interactable.gameObject);
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
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}
