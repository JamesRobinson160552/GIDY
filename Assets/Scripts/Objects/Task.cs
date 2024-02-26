using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private TaskManager manager;

    void Start()
    {
        manager = transform.parent.gameObject.GetComponent<TaskManager>();
    }
}
