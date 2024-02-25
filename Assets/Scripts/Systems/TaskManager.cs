using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject taskContainer;
    [SerializeField] private Vector3 topOfList;
    [SerializeField] private float taskGap = 200.0f;
    public void CreateNewTask()
    {
        //Get Transform of lowest task in list
        Transform lastTaskTransform = null;
        foreach (Transform task in taskContainer.transform)
        {
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
    }
}
