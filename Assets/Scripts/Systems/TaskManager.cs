using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject taskContainer;
    [SerializeField] private Vector3 topOfList;
    [SerializeField] private float taskGap = 200.0f;
    [SerializeField] private PlayerInfo playerInfo;

    void Awake()
    {
        LoadTasks();
    }

    //This buffer is needed since buttons cant call methods with multiple parameters
    public void MakeEmptyTask() 
    {
        CreateNewTask("Placeholder", "04-04-2002");
    }

    public void CreateNewTask(string taskName, string dueDate)
    {
        Debug.Log("Making new task");
        int numTasks = 0;

        //Get Transform of lowest task in list
        Transform lastTaskTransform = null;
        foreach (Transform task in taskContainer.transform)
        {
            numTasks++;
            lastTaskTransform = task;
        }

        GameObject newTask = Instantiate(taskPrefab, new Vector3(0,0,0), Quaternion.identity);
        newTask.transform.SetParent(taskContainer.transform);
        newTask.transform.localScale = new Vector3(1,1,1);

        if (lastTaskTransform == null) //no tasks in list
        {
            newTask.transform.position = topOfList;
        }

        else
        {
            //Put new task below last task
            Vector3 newPosition = new Vector3(
                lastTaskTransform.localPosition.x, 
                lastTaskTransform.localPosition.y-taskGap,
                lastTaskTransform.localPosition.z);
            Debug.Log("New Position is " + newPosition.ToString());
            newTask.transform.localPosition = newPosition;
            Debug.Log("Task put at " + newTask.transform.localPosition.ToString());
        }
        numTasks++;
        Task taskScript = newTask.GetComponent<Task>();
        taskScript. taskNumber = numTasks;
        taskScript.taskName = taskName;
        taskScript.dueDateString = dueDate;

        SaveTasks();
    }

    public void CompleteTask(int removedTaskNumber)
    {
        playerInfo.GainExp(10);
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
    }

    private string GetPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath+"/Data/"+"tasks.txt";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"tasks.txt";
        #else
        return Application.persistentDataPath+"/tasks.txt";
        #endif
    }

    public void SaveTasks()
    {
        StreamWriter writer = new StreamWriter(GetPath(), true);
        foreach (Transform taskTransform in taskContainer.transform)
        {
            Task task = taskTransform.gameObject.GetComponent<Task>();
            writer.WriteLine(task.WriteToString());
        }
        writer.Close();
    }

    public void LoadTasks()
    {
        StreamReader reader = new StreamReader(GetPath());
        while (reader.Peek() >= 0)
        {
            string[] taskInfo = reader.ReadLine().Split(',');
            Debug.Log(taskInfo);
            
        }
        reader.Close();
    }
}
