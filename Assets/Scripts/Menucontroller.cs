using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Menucontroller : MonoBehaviour {

    public GameObject menuBase;

    public GameObject menuBuy;
    public Dropdown menuBuyDropDown;
    public Text menuBuyBalance;
    public GameObject menuBuyScrollArea;

    public GameObject menuShow;
    public Dropdown menuShowDropDown;
    public Dropdown menuShowFilterDropDown;
    public GameObject menuShowScrollArea;

    public GameObject menuFP;
    public Dropdown menuFPDropDown;
    //public GameObject menuFPScrollAreaTop;
    public GameObject menuFPScrollArea;
    public GameObject assetBroker;

    // Prefabs
    public GameObject assetForSale;
    public GameObject assetForShow;
    public GameObject scheduleBackdrop;
    public GameObject aircraftHeader;
    public GameObject task;

   

    // Privates
    private Player player;
    private Clock clock;
    private GameObject[] displayedItems = null;
    
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<Clock>();
        HideMenu();
    }
	
	// Update is called once per frame
	void Update () {
        menuBuyBalance.text = player.GetBalance().ToString("N0", new NumberFormatInfo()
        {
            NumberGroupSizes = new[] { 3 },
            NumberGroupSeparator = "."
        }) + " $"; ;
    }

    public void ShowMenu(string tab)
    {
        menuBase.gameObject.SetActive(true);
        switch(tab)
        {
            case "buy":
                menuBuy.SetActive(true);
                menuShow.SetActive(false);
                menuFP.SetActive(false);
                UpdateCatalogue();
                break;
            case "show":
                menuBuy.SetActive(false);
                menuShow.SetActive(true);
                menuFP.SetActive(false);
                UpdateAssetList();
                break;
            case "fp":
                menuBuy.SetActive(false);
                menuShow.SetActive(false);
                menuFP.SetActive(true);
                UpdateFP();
                break;
        }
        
    }

    public void HideMenu()
    {
        menuBuy.SetActive(false);
        menuShow.SetActive(false);
        menuFP.SetActive(false);
        menuBase.gameObject.SetActive(false);
    }
    
    public void UpdateCatalogue()
    {
        List<Advertise> AdList = null;
        string category = "";

        foreach (Transform t in menuBuyScrollArea.transform)
        {
            GameObject.Destroy(t.gameObject);
        }

        if(menuBuyDropDown.value == 0)
        {
            AdList = assetBroker.GetComponent<JetBroker>().GetJetAdList().ConvertAll(x => (Advertise)x);
            category = "aircraft";
            
        } else if (menuBuyDropDown.value == 1)
        {
            AdList = assetBroker.GetComponent<AgeBroker>().GetJetAdList().ConvertAll(x => (Advertise)x);
            category = "age";
        }

        menuBuyScrollArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, AdList.Count * 110 + 10);

        displayedItems = new GameObject[AdList.Count];

        for (int i = 0; i < AdList.Count; i++)
        {
            displayedItems[i] = Instantiate(assetForSale);
            displayedItems[i].transform.SetParent(menuBuyScrollArea.transform, false);
            displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-i) * 110 - 10);

            // Set Data
            displayedItems[i].transform.Find("Name").GetComponent<Text>().text = AdList[i].Name;
            displayedItems[i].transform.Find("Price").GetComponent<Text>().text = AdList[i].Price.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = "."
            }) + " $";
            displayedItems[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + AdList[i].Name);
            displayedItems[i].transform.Find("Description").GetComponent<Text>().text = AdList[i].Description;
            string nameOfAsset = AdList[i].Name;
            string categoryOfAsset = category;
            int priceOfAsset = AdList[i].Price;
            displayedItems[i].transform.Find("Buybutton").GetComponent<Button>().onClick.AddListener(delegate { player.BuyAsset(nameOfAsset, categoryOfAsset, priceOfAsset); });
        }
    }
    
    public void UpdateAssetList()
    {
        //TODO Update shown infos via Clock
        List<GameObject> showList = new List<GameObject>();
        menuShowFilterDropDown.ClearOptions();

        foreach (Transform t in menuShowScrollArea.transform)
        {
            GameObject.Destroy(t.gameObject);
        }

        if (menuShowDropDown.value == 0)
        {
            menuShowFilterDropDown.AddOptions(new List<string> { "All" });
            menuShowFilterDropDown.AddOptions(player.GetTypesInService());
            
            showList = player.GetFilteredJetList(menuShowFilterDropDown.captionText.text);

            displayedItems = new GameObject[showList.Count];

            menuShowScrollArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, showList.Count * 110 + 10);
            
            for (int i = 0; i < showList.Count; i++)
            {
                displayedItems[i] = Instantiate(assetForShow);
                displayedItems[i].transform.SetParent(menuShowScrollArea.transform, false);
                displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-i) * 110 - 10);

                displayedItems[i].transform.Find("Tailnumber").GetComponent<Text>().text = showList[i].GetComponent<Jet>().Tailnumber;
                displayedItems[i].transform.Find("Type").GetComponent<Text>().text = showList[i].GetComponent<Jet>().Type;
                displayedItems[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + showList[i].GetComponent<Jet>().Type);
                displayedItems[i].transform.Find("Serviceability").GetComponent<Text>().text = showList[i].GetComponent<Jet>().IsServiceable ? "serviceable" : "unserviceable";
                displayedItems[i].transform.Find("LifingType").GetComponent<Text>().text = showList[i].GetComponent<Jet>().LifingType;
                displayedItems[i].transform.Find("LifingCount").GetComponent<Text>().text = showList[i].GetComponent<Jet>().Lifing.ToString();
                displayedItems[i].transform.Find("Location").GetComponent<Text>().text = showList[i].GetComponent<Jet>().Location;
                displayedItems[i].transform.Find("AgeCategory").GetComponent<Text>().text = "";
                displayedItems[i].transform.Find("Airborne").GetComponent<Text>().text = showList[i].GetComponent<Jet>().IsAirborne ? "airborne" : "";
            }
        } else if (menuShowDropDown.value == 1)
        {
            menuShowFilterDropDown.AddOptions(new List<string> { "All" });
            menuShowFilterDropDown.AddOptions(player.GetAgeCategoriesInService());

            showList = player.GetFilteredAgeList(menuShowFilterDropDown.captionText.text);

            displayedItems = new GameObject[showList.Count];

            menuShowScrollArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, showList.Count * 110 + 10);

            for (int i = 0; i < showList.Count; i++)
            {
                displayedItems[i] = Instantiate(assetForShow);
                displayedItems[i].transform.SetParent(menuShowScrollArea.transform, false);
                displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-i) * 110 - 10);

                displayedItems[i].transform.Find("Tailnumber").GetComponent<Text>().text = showList[i].GetComponent<Age>().Serialnumber;
                displayedItems[i].transform.Find("Type").GetComponent<Text>().text = showList[i].GetComponent<Age>().Type;
                displayedItems[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + showList[i].GetComponent<Age>().Type);
                displayedItems[i].transform.Find("Serviceability").GetComponent<Text>().text = showList[i].GetComponent<Age>().Serviceability ? "serviceable" : "unserviceable";
                displayedItems[i].transform.Find("LifingType").GetComponent<Text>().text = showList[i].GetComponent<Age>().LifingType=="no" ? "": showList[i].GetComponent<Age>().LifingType;
                displayedItems[i].transform.Find("LifingCount").GetComponent<Text>().text = showList[i].GetComponent<Age>().LifingType == "no" ? "" : showList[i].GetComponent<Age>().Lifing.ToString();
                displayedItems[i].transform.Find("Location").GetComponent<Text>().text = showList[i].GetComponent<Age>().Location;
                displayedItems[i].transform.Find("AgeCategory").GetComponent<Text>().text = "Category: " + showList[i].GetComponent<Age>().AgeCategory;
                displayedItems[i].transform.Find("Airborne").GetComponent<Text>().text = "";
            }

        }
        
    }

    public void UpdateFP()
    {
        GameObject taskBuffer;
        List<GameObject> flightsToShow = new List<GameObject>();
        List<TopTask> flightList = new List<TopTask>();

        menuFPDropDown.ClearOptions();
        menuFPDropDown.AddOptions(new List<string> { "All" });
        menuFPDropDown.AddOptions(player.GetTypesInService());
        /*
        if (displayedItems != null)
        {
            foreach (var i in displayedItems)
            {
                GameObject.Destroy(i);
            }

        }
        */
        
        foreach (Transform t in menuFPScrollArea.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
        

        menuFPScrollArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (24 * 60) + 50);
                
        for (int i = 0; i < 24; i++)
        {
            GameObject backDrop = Instantiate(scheduleBackdrop);
            backDrop.transform.SetParent(menuFPScrollArea.transform, false);
            backDrop.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-i * 60) - 50);
            backDrop.transform.Find("HourLabel").GetComponent<Text>().text = i.ToString().PadLeft(2, '0') + ":00";
        }
        

        List<string> jetsToShow = player.GetTailnumbersOfJetsWithFlightTask(menuFPDropDown.captionText.text);
        menuFPScrollArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50 + (jetsToShow.Count * 160));
        
        displayedItems = new GameObject[jetsToShow.Count];
        
        for (int i = 0; i < jetsToShow.Count; i++)
        {
            displayedItems[i] = Instantiate(aircraftHeader);
            displayedItems[i].transform.SetParent(menuFPScrollArea.transform, false);
            displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 160 + 50, 0);

            displayedItems[i].transform.Find("TN").GetComponent<Text>().text = jetsToShow[i];
            displayedItems[i].transform.Find("Type").GetComponent<Text>().text = player.GetTypeForTailnumber(jetsToShow[i]);
            flightList.AddRange(player.GetTasksOfTypeForTailNumber("Flight", jetsToShow[i]));
            foreach (var flight in flightList)
            {
                if(clock.CheckIfToday(flight.StartTime))
                {
                    taskBuffer = Instantiate(task);
                    taskBuffer.transform.SetParent(menuFPScrollArea.transform, false);
                    taskBuffer.transform.Find("Name").GetComponent<Text>().text = flight.Taskname;
                    taskBuffer.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 160 + 50, -flight.StartTime.Hour * 60 - flight.StartTime.Minute -50);
                    taskBuffer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, flight.OriginalDuration);
                }
            }
            flightList.Clear();
        }
        
    }
}
