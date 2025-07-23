using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    public bool isOpen;
    public bool isFull;

    // Pickup Pop Up
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;

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

    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemList.Add(itemName);

        // Ambil sprite jika ada
        Sprite itemSprite = null;
        Image imageComponent = itemToAdd.GetComponent<Image>();
        if (imageComponent != null)
        {
            itemSprite = imageComponent.sprite;
        }

        TriggerPickupPopUp(itemName, itemSprite);
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        if (pickupAlert != null)
        {
            pickupAlert.SetActive(true);
            pickupName.text = itemName;

            if (itemSprite != null)
            {
                pickupImage.sprite = itemSprite;
                pickupImage.enabled = true;
            }
            else
            {
                pickupImage.enabled = false;
            }

            // Auto-hide popup
            StopAllCoroutines();
            StartCoroutine(HidePickupPopupAfterDelay(2f));
        }
    }

    IEnumerator HidePickupPopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickupAlert.SetActive(false);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject(); // jika tidak ada slot kosong
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        return counter >= 21;
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;
        for (int i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter > 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }
    }

    public void ReCalculateList()
    {
        itemList.Clear(); // perbaikan agar tidak dobel

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name; // contoh: "Stone(Clone)"
                string result = name.Replace("(Clone)", "").Trim();
                itemList.Add(result);
            }
        }
    }
}
