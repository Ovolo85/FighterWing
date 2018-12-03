using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age : MonoBehaviour {

    public string Type { get; private set; }
    public string Serialnumber { get; private set; }
    public bool Serviceability { get; private set; }
    public string LifingType { get; private set; }
    public int Lifing { get; private set; }
    public string Location { get; private set; }
    public string AgeCategory { get; private set; }

    // Use this for initialization
    void Start () {
        Location = "Hangar 01";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initiate (string t, string tn, string lt, string cat)
    {
        Serviceability = true;
        Type = t;
        LifingType = lt;
        Lifing = 0;
        Serialnumber = tn;
        AgeCategory = cat;
    }
}
