using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private TaskManager manager;
    public int taskNumber;
    public string taskName;
    public string dueDateString;

    public void Complete()
    {
        manager = transform.parent.gameObject.GetComponent<TaskManager>();
        manager.CompleteTask(taskNumber);
        Destroy(this.gameObject);
    }

    public string WriteToString()
    {
        return taskName + ',' + dueDateString;
    }
}
