using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private TMP_InputField taskNameEditText;
    [SerializeField] private TMP_InputField dueDateEditText;

    void Start()
    {
        manager = transform.parent.gameObject.GetComponent<TaskManager>();
        SetInfo();
    }

    void SetInfo()
    {
        taskNameText.text = taskName;
        dueDateText.text = dueDateString;
        taskNameEditText.text= taskName;
        dueDateEditText.text = dueDateString; 
    }

    public void Complete()
    {
        UiSounds.instance.PlaySound(4);
        manager.CompleteTask(taskNumber, true);
    }

    public string WriteToString()
    {
        return taskName + ',' + dueDateString;
    }

    public void EnterEditView()
    {
        UiSounds.instance.PlaySound(0);
        //Debug.Log("Entering edit view");
        dueDateEditText.text = dueDateString;
        taskNameEditText.text = taskName;
        editView.SetActive(true);
        mainView.SetActive(false);
    }

    public void SaveEdits()
    {
        //Debug.Log("Saved Edits");
        editView.SetActive(false);
        mainView.SetActive(true);
        dueDateString = dueDateEditText.text;
        taskName = taskNameEditText.text;
        SetInfo();
        TaskManager.i.SaveTask(taskNumber, taskName, dueDateString);
    }

    public void DiscardEdits()
    {
        //Debug.Log("Discarding edits");
        editView.SetActive(false);
        mainView.SetActive(true);
    }

    public void RemoveTask()
    {
        //Debug.Log("Removing Task");
        manager.CompleteTask(taskNumber, false);
        Destroy(this.gameObject);
    }
}
