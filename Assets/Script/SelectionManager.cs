using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public float interactionDistance = 8f;
    public Transform player;

    private TextMeshProUGUI interaction_text;
    public Image centerDotImage;
    public Image handIcon;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponentInChildren<TextMeshProUGUI>();

        if (interaction_text == null)
        {
            Debug.LogError("TextMeshProUGUI not found in interaction_Info_UI");
        }

        interaction_Info_UI.SetActive(false);
        if (handIcon != null) handIcon.gameObject.SetActive(false);
        if (centerDotImage != null) centerDotImage.gameObject.SetActive(true);
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

                    // Ganti ikon saat melihat objek yang bisa diambil
                    if (selectionTransform.CompareTag("Item"))
                    {
                        centerDotImage.gameObject.SetActive(false);
                        handIcon.gameObject.SetActive(true);
                    }
                    else
                    {
                        handIcon.gameObject.SetActive(false);
                        centerDotImage.gameObject.SetActive(true);
                    }

                    // Ambil item jika klik kiri
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
                            handIcon.gameObject.SetActive(false);
                            centerDotImage.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    HideUI();
                }
            }
            else
            {
                HideUI();
            }
        }
        else
        {
            HideUI();
        }
    }

    void HideUI()
    {
        interaction_Info_UI.SetActive(false);
        if (handIcon != null) handIcon.gameObject.SetActive(false);
        if (centerDotImage != null) centerDotImage.gameObject.SetActive(true);
    }
}
