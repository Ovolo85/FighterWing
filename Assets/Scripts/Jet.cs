using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour {

    public string Type { get; private set; }
    public string Tailnumber { get; private set; }
    public bool IsServiceable { get; private set; }
    public string LifingType { get; private set; }
    public int Lifing { get; private set; }
    public bool IsAirborne { get; private set; }
    public string Location { get; private set; }



    // Use this for initialization
    void Start () {
        
        LifingType = "FH";
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Initiate(string t, string tn)
    {
        IsServiceable = true;
        Type = t;
        Tailnumber = tn;
        Lifing = 0;
        
    }

    public void TakeOff()
    {
        IsAirborne = true;
        IsServiceable = false;
    }

    public void Land()
    {
        Location = "Taxiway";
        IsAirborne = false;

    }

    public void TakeOffFerryInbound()
    {
        Location = "Civil Airspace";
        IsAirborne = true;
        IsServiceable = false;
        
    }

}
