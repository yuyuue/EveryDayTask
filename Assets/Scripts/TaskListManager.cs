using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject taskItemPrefab;
    public TMP_InputField newTaskNameInput;

    private List<Task> taskList = new List<Task>();
    private List<int> allTaskIds = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        LoadTaskList();
        UpdateTaskList();
    }

    public void AddTask()
    {
        string name = newTaskNameInput.text;
        if (name != "")
        {
            //最新IDから新タスクのIDを作成
            int newTaskId = PlayerPrefs.GetInt("latestID") + 1;
            allTaskIds.Add(newTaskId);

            //新IDから新タスクのキー情報を作成
            string taskNameKey = "task" + newTaskId.ToString();
            string addDate = System.DateTime.Now.ToString("yy/MM/dd");

            //タスクの情報を保存
            PlayerPrefs.SetString(taskNameKey + "name", name);
            PlayerPrefs.SetInt(taskNameKey + "Count", 0);
            PlayerPrefs.SetString(taskNameKey + "LatestDate", addDate);

            //IDの情報を保存
            string idsString = string.Join(",", allTaskIds);
            PlayerPrefs.SetString("AllTaskIds", idsString);
            PlayerPrefs.SetInt("latestID", newTaskId);

            PlayerPrefs.Save();

            taskList.Add(new Task(newTaskId, name, 0, addDate));

            newTaskNameInput.text = "";
        }
    }

    public void DeleteTask()
    {

    }

    public void CompleteTask(int taskId, int compCount, string latestDate)
    {
        Task task = taskList.Find(task => task.id == taskId);
        task.compCount = compCount;
        task.completionDate = latestDate;
    }

    private void LoadTaskList()
    {
        //IDを全て取得
        string allTaskIds = "";
        allTaskIds = PlayerPrefs.GetString("AllTaskIds", "");

        if (!string.IsNullOrEmpty(allTaskIds))
        {
            int[] taskIds = allTaskIds.Split(',').Select(int.Parse).ToArray();
            //IDごとにタスク情報を取得してタスクリストを作成
            foreach (var taskId in taskIds)
            {
                string taskName = PlayerPrefs.GetString("task" + taskId + "name", "");
                int taskCount = PlayerPrefs.GetInt("task" + taskId + "Count");
                string taskLatestDate = PlayerPrefs.GetString("task" + taskId + "LatestDate", "");

                taskList.Add(new Task(taskId, taskName, taskCount, taskLatestDate));
            }
        }
    }

    public void UpdateTaskList()
    {
        foreach (Transform task in contentPanel)
        {
            Destroy(task.gameObject);
        }

        foreach (Task task in taskList)
        {
            GameObject newTask = Instantiate(taskItemPrefab, contentPanel);
            newTask.GetComponent<TaskItem>().Setup(task);
        }
    }
}

[System.Serializable]
public class Task
{
    public int id;
    public string name;
    public int compCount;
    public string completionDate;

    public Task(int id, string name, int compCount, string completionDate)
    {
        this.id = id;
        this.name = name;
        this.compCount = compCount;
        this.completionDate = completionDate;
    }
}
