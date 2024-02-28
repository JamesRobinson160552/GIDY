using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject taskContainer;
    [SerializeField] private Vector3 topOfList;
    [SerializeField] private float taskGap = 200.0f;
    [SerializeField] private PlayerInfo playerInfo;
    public void CreateNewTask()
    {
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
        newTask.GetComponent<Task>().taskNumber = numTasks;
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
}
