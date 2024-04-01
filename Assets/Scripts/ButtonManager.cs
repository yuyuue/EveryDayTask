using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject taskListPanel;
    public GameObject taskAddPanel;
    public GameObject taskEditPanel;
    public GameObject taskListManager;

    public void GoToAddMenu()
    {
        taskListPanel.SetActive(false);
        taskAddPanel.SetActive(true);
        taskEditPanel.SetActive(false);
    }

    public void GoToEditMenu()
    {
        taskListPanel.SetActive(false);
        taskAddPanel.SetActive(false);
        taskEditPanel.SetActive(true);
    }

    public void GoToTaskList()
    {
        taskListPanel.SetActive(true);
        taskAddPanel.SetActive(false);
        taskEditPanel.SetActive(false);
        taskListManager.GetComponent<TaskListManager>().UpdateTaskList();
    }

    public void CompleteTask(int taskId)
    {
        //CompCountとLatestDateを更新
        string taskCountKey = "task" + taskId + "Count";
        int taskCount = PlayerPrefs.GetInt(taskCountKey) + 1;
        PlayerPrefs.SetInt(taskCountKey, taskCount);

        string taskLatestDateKey = "task" + taskId + "LatestDate";
        string latestDate = System.DateTime.Now.ToString("yy/MM/dd");
        PlayerPrefs.SetString(taskLatestDateKey, latestDate);

        PlayerPrefs.Save();

        //タスクリストを更新
        taskListManager.GetComponent<TaskListManager>().CompleteTask(taskId, taskCount, latestDate);
        taskListManager.GetComponent<TaskListManager>().UpdateTaskList();
    }
}
