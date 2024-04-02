using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

public class TaskManager : MonoBehaviour, ISavable
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject taskContainer;
    [SerializeField] private Vector3 topOfList;
    [SerializeField] private float taskGap = 200.0f;
    [SerializeField] private PlayerInfo playerInfo;
    public List<TaskStruct> tasks = new List<TaskStruct>();
    public int numTasks = 0;
    public static TaskManager i { get; private set; }

    void Start()
    {
        if (i == null) i = this;
        SavingSystem.i.Load("tasks");
    }

    //This buffer is needed since buttons cant call methods with multiple parameters
    public void MakeEmptyTask() 
    {
        Task newTask = CreateNewTask("", "");
        tasks.Add(new TaskStruct("", ""));
        newTask.EnterEditView();
    }

    public Task CreateNewTask(string taskName, string dueDate)
    {
        Debug.Log("Creating new task");
        Vector3 newTaskPosition = new Vector3(0,0,0);

        if (numTasks == 0)
        {
            newTaskPosition = topOfList;
        }

        else
        {
            Transform lastTaskTransform = null;
            foreach (Transform task in taskContainer.transform)
            {
                lastTaskTransform = task;
            }
            newTaskPosition = new Vector3(0, lastTaskTransform.localPosition.y - taskGap, 0);
        }
 
        GameObject newTask = Instantiate(taskPrefab, new Vector3(0,0,0), Quaternion.identity);
        newTask.transform.SetParent(taskContainer.transform);
        newTask.transform.localScale = new Vector3(1,1,1);
        newTask.transform.localPosition = newTaskPosition;

        numTasks++;
        Task taskScript = newTask.GetComponent<Task>();
        taskScript.taskName = taskName;
        taskScript.dueDateString = dueDate;
        taskScript.taskNumber = numTasks;
        return taskScript;
    }

    public void CompleteTask(int removedTaskNumber, bool wasFinished)
    {
        int num = removedTaskNumber - 1;
        if (wasFinished) { playerInfo.GainExp(10); }
        //Move all tasks below removed task up in the list
        foreach (Transform taskTransform in taskContainer.transform)
        {
            Task task = taskTransform.gameObject.GetComponent<Task>();
            if (task.taskNumber > removedTaskNumber)
            {
                task.taskNumber--;
                taskTransform.localPosition = new Vector3(
                    taskTransform.localPosition.x,
                    taskTransform.localPosition.y + taskGap,
                    taskTransform.localPosition.z
                );
            }
        }
        tasks.RemoveAt(num);
        numTasks--;
        SavingSystem.i.Save("tasks");
        Debug.Log("Removed task " + removedTaskNumber);
        Destroy(transform.GetChild(num).gameObject);
    }

    public object CaptureState()
    {
        //Debug.Log("Capturing state of tasks");
        Dictionary<string, object> state = new Dictionary<string, object>();
        for (int i = 0; i < tasks.Count; i++)
        {
            state[i.ToString()] = tasks[i];
        }
        return state;
    }

    public void RestoreState(object state)
    {
        Debug.Log(state);
        tasks = new List<TaskStruct>();
        Debug.Log("Restoring state of tasks");
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        foreach (string key in stateDict.Keys)
        {
            TaskStruct task = (TaskStruct)stateDict[key];
            tasks.Add(task);
        }
        foreach (TaskStruct task in tasks)
        {
            CreateNewTask(task.taskName, task.dueDateString);
        }
    }

    public void SaveTask(int taskNumber, string taskName, string dueDateString)
    {
        tasks[taskNumber-1] = new TaskStruct(taskName, dueDateString);
        SavingSystem.i.Save("tasks");
    }

    [System.Serializable]
    public struct TaskStruct
    {
        public TaskStruct(string taskName, string dueDateString)
        {
            this.taskName = taskName;
            this.dueDateString = dueDateString;
        }
        [SerializeField] public string taskName { get; set; }
        [SerializeField] public string dueDateString { get; set; }
    }
}
