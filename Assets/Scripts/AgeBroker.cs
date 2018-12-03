using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeBroker : MonoBehaviour {

    public TextAsset assetList;

    private string[] AdListRaw;
    private List<AgeAdvertise> AgeAdList;

    // Use this for initialization
    void Start()
    {
        AgeAdList = new List<AgeAdvertise>();

        AdListRaw = assetList.text.Split("\n"[0]);

        BuildAgeAdList(AdListRaw);


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BuildAgeAdList(string[] rawList)
    {
        AgeAdList.Clear();
        foreach (var s in rawList)
        {
            string[] line = s.Split(","[0]);
            if (line[1] == "age")
            {
                AgeAdList.Add(new AgeAdvertise(line));
            }
        }

    }

    public List<AgeAdvertise> GetJetAdList()
    {
        return AgeAdList;
    }

    public GameObject Buy(string name, string serialnumber)
    {
        GameObject result = GameObject.Instantiate(Resources.Load("age/" + name)) as GameObject;
        string lifingtype = GetLifingTypeOfAge(name);
        string category = GetCategoryOfAge(name);

        result.GetComponent<Age>().Initiate(name, serialnumber, lifingtype, category);

        return result;
    }

    public string GetLifingTypeOfAge (string name)
    {
        string result = "";

        foreach(var age in AgeAdList)
        {
            if (age.Name == name) { result = age.LifingType;  }
        }

        return result;
    }

    public string GetCategoryOfAge (string name)
    {
        foreach(var age in AgeAdList)
        {
            if (age.Name == name)
            {
                return age.AgeCategory;
            }
        }
        return "";
    }

    public string GetSerialNumberPrefix(string name)
    {
        foreach (var age in AgeAdList)
        {
            if (age.Name == name)
            {
                return age.Serialnumber;
            }
        }
        return "";
    }
}
