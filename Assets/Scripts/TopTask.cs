using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTask {

    public string Taskname { get; private set; }
    private TimeValue startTime;
    public int Duration { get; private set; }
    public int OriginalDuration { get; private set; }
    public string TaskType { get; private set; }
    public GameObject AssignedJet { get; private set; }

    private bool isActive;

    public TopTask(string n, string tt, TimeValue st, int d, GameObject jet)
    {
        this.Taskname = n;
        this.startTime = st;
        this.Duration = d;
        this.OriginalDuration = d;
        this.TaskType = tt;
        this.AssignedJet = jet;

        this.isActive = true;
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool UpdateDuration ()
    {
        if(this.isActive)
        {
            this.Duration--;
            
            //Debug.Log("Duration of " + Taskname + ": " + Duration.ToString());
        }        
        return Duration == 0;
    }
}
