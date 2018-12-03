using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBroker : MonoBehaviour {

    public TextAsset assetList;

    private string[] AdListRaw;
    private List<JetAdvertise> JetAdList;

    // Use this for initialization
    void Start () {
        JetAdList = new List<JetAdvertise>();

        AdListRaw = assetList.text.Split("\n"[0]);

        BuildJetAdList(AdListRaw);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BuildJetAdList(string[] rawList)
    {
        JetAdList.Clear();
        foreach(var s in rawList)
        {
            string[] line = s.Split(","[0]);
            if(line[1] == "aircraft")
            {
                JetAdList.Add(new JetAdvertise(line));
            }
        }
        
    }

    public List<JetAdvertise> GetJetAdList ()
    {
        return JetAdList;
    }

    public string GetTailNumberPrefix(string type)
    {
        foreach(var data in JetAdList)
        {
            if (data.Name == type)
            {
                return data.Serialnumber;
            }
        }
        return "";
    }

    public GameObject Buy(string name, string tailnumber) 
    {
        GameObject result = GameObject.Instantiate(Resources.Load("aircraft/" + name)) as GameObject;

        result.GetComponent<Jet>().Initiate(name, tailnumber);
        
        return result;
    }
}
