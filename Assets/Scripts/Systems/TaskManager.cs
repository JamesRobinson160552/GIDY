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
    public int numTasks;
    public static TaskManager i { get; private set; }

    void Awake()
    {
        if (i == null) i = this;
        SavingSystem.i.Load("tasks");
    }

    //This buffer is needed since buttons cant call methods with multiple parameters
    public void MakeEmptyTask() 
    {
        Task newTask = CreateNewTask("", "");
        newTask.EnterEditView();
    }

    public Task CreateNewTask(string taskName, string dueDate)
    {
        //Debug.Log("Creating new task");
        Vector3 newTaskPosition = new Vector3(0,0,0);
        numTasks = taskContainer.transform.childCount;

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
        numTasks--;
        tasks.RemoveAt(removedTaskNumber-1);
        SavingSystem.i.Save("tasks");
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
        tasks.Clear();
        //Debug.Log("Restoring state of tasks");
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        for (int i = 0; i < stateDict.Count; i++)
        {
            TaskStruct task = (TaskStruct)stateDict[i.ToString()];
            tasks.Add(task);
            CreateNewTask(task.taskName, task.dueDateString);
        }
    }

    public void SaveTask(int taskNumber, string taskName, string dueDateString)
    {
        if (taskNumber > tasks.Count) tasks.Add(new TaskStruct(taskName, dueDateString));
        else tasks[taskNumber-1] = new TaskStruct(taskName, dueDateString);
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
