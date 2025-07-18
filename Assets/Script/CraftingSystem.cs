using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string> ();

    //Category Buttons
    Button toolsBTN;

    //Craft Buttons
    Button craftAxeBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;

    public bool isOpen;

    //All Blueprints



    public static CraftingSystem Instance { get; set; }


    private void Awake()
    {
        if (Instance !=null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button> ();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        // AXE
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("Req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("Req2").GetComponent<TextMeshProUGUI>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("CraftButton").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });

    }

   
    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive (false);
        toolsScreenUI.SetActive (true);
    }


    void CraftAnyItem()
    {

        //add item into inventory


        //remove resources from inventory






    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }



    }
}