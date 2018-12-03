using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // Player owns it all
    public Text moneyText;
    public GameObject messageBroker;
    public GameObject taskBroker;
    public GameObject assetBroker;
    public GameObject clock;

    private int money = 10000000;
    private List<GameObject> jetInventory;
    private List<GameObject> ageInventory;

    private MessageBroker messageBrokerScript;
    private JetBroker jetBrokerScript;
    private AgeBroker ageBrokerScript;
    private TaskBroker taskBrokerScript;
    private Clock clockScript;

    // Use this for initialization
    void Start () {
        jetInventory = new List<GameObject>();
        ageInventory = new List<GameObject>();

        messageBrokerScript = messageBroker.GetComponent<MessageBroker>();
        jetBrokerScript = assetBroker.GetComponent<JetBroker>();
        ageBrokerScript = assetBroker.GetComponent<AgeBroker>();
        taskBrokerScript = taskBroker.GetComponent<TaskBroker>();
        clockScript = clock.GetComponent<Clock>();

        UpdateMoneyDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMoneyDisplay()
    {
        moneyText.text = money.ToString("N0", new NumberFormatInfo()
        {
            NumberGroupSizes = new[] { 3 },
            NumberGroupSeparator = "."
        }) + " $";
    }
    
    public void BuyAsset(string name, string cat, int price)
    {
        if (PerformTransaction(-price))
        {
            
            switch (cat)
            {
                case "aircraft":
                    string tailnumber = jetBrokerScript.GetTailNumberPrefix(name) + (GetJetCount(name)+1).ToString().PadLeft(2, '0');
                    GameObject newJet = jetBrokerScript.Buy(name, tailnumber);
                    taskBrokerScript.AssignFlightTask("Ferry inbound", clockScript.GetCurrentTime(), 90, newJet);
                    newJet.GetComponent<Jet>().TakeOffFerryInbound();
                    jetInventory.Add(newJet);
                    break;
                case "age":
                    string serialnumber = ageBrokerScript.GetSerialNumberPrefix(name) + (GetAgeCount(name)+1).ToString().PadLeft(2, '0');
                    ageInventory.Add(ageBrokerScript.Buy(name, serialnumber));
                    break;
            }
            
            messageBrokerScript.ReleaseMessage("Your " + name + " is in delivery...");
        }
        
    }

    public bool PerformTransaction (int value)
    {
        if (money + value >= 0)
        {
            money += value;
            UpdateMoneyDisplay();
            return true;
        } else
        {
            return false;
        }
    }

    public int GetBalance ()
    {
        return money;
    }

    public int GetJetCount(string name)
    {
        int count = 0;
        foreach (var jet in jetInventory)
        {
            if (jet.GetComponent<Jet>().Type == name) { count++; }
        }
        return count;
    }

    public int GetAgeCount(string name)
    {
        int count = 0;
        foreach (var age in ageInventory)
        {
            if(age.GetComponent<Age>().Type == name) { count++; }
        }
        return count;
    }

    public List<GameObject> GetFilteredJetList (string type)
    {
        List<GameObject> result = new List<GameObject>();
        if(GetTypesInService().Contains(type))
        {
            foreach(var jet in jetInventory)
            {
                if(jet.GetComponent<Jet>().Type == type)
                {
                    result.Add(jet);
                }
            }
        } else
        {
            result = jetInventory;
        }
        
        return result;
    }

    public List<GameObject> GetFilteredAgeList (string ageCat)
    {
        List<GameObject> result = new List<GameObject>();
        if (GetAgeCategoriesInService().Contains(ageCat))
        {
            foreach (var age in ageInventory)
            {
                if (age.GetComponent<Age>().AgeCategory == ageCat)
                {
                    result.Add(age);
                }
            }
        }
        else
        {
            result = ageInventory;
        }

        return result;
    }

    public List<string> GetTypesInService ()
    {
        List<string> result = new List<string>();
        foreach (var jet in jetInventory)
        {
            if(!result.Contains(jet.GetComponent<Jet>().Type))
            {
                result.Add(jet.GetComponent<Jet>().Type);
            }
        }
        return result;
    }

    public List<string> GetAgeCategoriesInService()
    {
        List<string> result = new List<string>();
        foreach(var age in ageInventory)
        {
            if(!result.Contains(age.GetComponent<Age>().AgeCategory))
            {
                result.Add(age.GetComponent<Age>().AgeCategory);
            }
        }
        return result;
    }

    public List<string> GetTailnumbersOfJetsWithFlightTask(string typeFilter)
    {
        //TODO Liste sortiert zurück geben
        List<GameObject> jetList = taskBrokerScript.GetJetsWithFlightTask();
        List<string> result = new List<string>();
        foreach(var jet in jetList)
        {
            if (GetTypesInService().Contains(typeFilter))
            {
                if(jet.GetComponent<Jet>().Type == typeFilter)
                {
                    result.Add(jet.GetComponent<Jet>().Tailnumber);
                }
            } else
            {
                result.Add(jet.GetComponent<Jet>().Tailnumber);
            }
            
        }
        return result;
    }

    public List<TopTask> GetFlightTasksForTailNumber (string tn)
    {
        List<TopTask> result = new List<TopTask>();

        return result;
    }

    public string GetTypeForTailnumber(string tn)
    {
        foreach(var jet in jetInventory)
        {
            if (jet.GetComponent<Jet>().Tailnumber == tn)
            {
                return jet.GetComponent<Jet>().Type;
            }
        }
        return "";
    }
}
