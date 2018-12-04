using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBroker : MonoBehaviour {

    public GameObject messageBroker;
    
    private List<TopTask> taskList;
    private List<TopTask> taskDeletionList;
    private MessageBroker messageBrokerScript;

    // Use this for initialization
    void Start () {
        taskList = new List<TopTask>();
        taskDeletionList = new List<TopTask>();

        messageBrokerScript = messageBroker.GetComponent<MessageBroker>();


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Update_TaskTimes()
    {
        foreach (var t in taskList)
        {
           if (t.UpdateDuration())
            {
                print("Kill " + t.Taskname);
                taskDeletionList.Add(t);
                               
            }
        }
        foreach (var dt in taskDeletionList)
        {
            taskList.Remove(dt);
            messageBrokerScript.ReleaseMessage(dt.Taskname + " finished!");
        }
        taskDeletionList.Clear();

    }

    public void AssignFlightTask(string flightType, TimeValue startTime, int duration, GameObject jet )
    {
        TopTask flightTask = new TopTask(flightType, "Flight", startTime, duration, jet);
        taskList.Add(flightTask);
    }

    public List<GameObject> GetJetsWithFlightTask()
    {
        List<GameObject> result = new List<GameObject>();
        foreach(var task in taskList)
        {
            if(task.TaskType =="Flight" && !result.Contains(task.AssignedJet))
            {
                result.Add(task.AssignedJet);
            }
        }
        return result;
    }

    public List<TopTask> GetFlightsForJet (string tn)
    {
        List<TopTask> result = new List<TopTask>();

        foreach (var task in taskList)
        {
            if (task.TaskType == "Flight" && task.AssignedJet.GetComponent<Jet>().Tailnumber == tn)
            {
                result.Add(task);
            }
        }

        return result;
    }
}
