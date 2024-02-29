using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject taskContainer;
    [SerializeField] private Vector3 topOfList;
    [SerializeField] private float taskGap = 200.0f;
    [SerializeField] private PlayerInfo playerInfo;
    public int numTasks;

    void Awake()
    {
        LoadTasks();
        AssetDatabase.Refresh();
    }

    //This buffer is needed since buttons cant call methods with multiple parameters
    public void MakeEmptyTask() 
    {
        CreateNewTask("Placeholder", "04-04-2002");
    }

    public void CreateNewTask(string taskName, string dueDate)
    {
        Vector3 newTaskPosition = new Vector3(0,0,0);
        numTasks = taskContainer.transform.childCount;
        Debug.Log(numTasks.ToString() + " tasks in list");

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
        SaveTasks();
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
        using (var stream = new FileStream(GetPath(), FileMode.Truncate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (Transform taskTransform in taskContainer.transform)
                {
                    Task task = taskTransform.gameObject.GetComponent<Task>();
                    writer.WriteLine(task.WriteToString());
                }
                writer.Close();
            }
            stream.Close();
        }
    }

    public void LoadTasks()
    {
        //Get tasks from file
        StreamReader reader = new StreamReader(GetPath());
        List<string> tasks = new List<string>();
        while (reader.Peek() >= 0)
        {
            tasks.Add(reader.ReadLine());
        }
        reader.Close();

        //Create tasks in game
        foreach (string task in tasks)
        {
            string[] data = task.Split(',');
            string n = data[0];
            string d = data[1];
            CreateNewTask(n, d);
        }
    }
}
