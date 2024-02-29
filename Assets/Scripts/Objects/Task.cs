using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{
    private TaskManager manager;
    public int taskNumber;
    public string taskName;
    public string dueDateString;
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject editView;
    [SerializeField] private TextMeshProUGUI taskNameText;
    [SerializeField] private TextMeshProUGUI dueDateText;

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

    public void EnterEditView()
    {
        Debug.Log("Entering edit view");
        editView.SetActive(true);
        mainView.SetActive(false);
    }

    public void SaveEdits()
    {
        Debug.Log("Saved Edits");
        editView.SetActive(false);
        mainView.SetActive(true);
    }

    public void DiscardEdits()
    {
        Debug.Log("Discarding edits");
        editView.SetActive(false);
        mainView.SetActive(true);
    }

    public void RemoveTask()
    {
        Debug.Log("Removing Task");
    }
}
