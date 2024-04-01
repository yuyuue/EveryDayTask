using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    //タスク内の要素をインスペクターで設定
    public TextMeshProUGUI nameText;    //タスク名
    public TextMeshProUGUI countText;   //達成回数
    public TextMeshProUGUI dateText;    //最終達成日付
    public Button completedButton;      //達成ボタン
    public Button goEditButton;         //Edit button

    //マネージャー要素を宣言
    public GameObject buttonManager;    //button マネージャー

    //自分のタスクの情報
    private int id;
    private string taskName;
    private int compCount;
    private string completionDate;

    public void Setup(Task taskData)
    {
        this.id = taskData.id;
        this.taskName = taskData.name;
        this.compCount = taskData.compCount;
        this.completionDate = taskData.completionDate;

        nameText.text = taskName;
        countText.text = compCount.ToString();
        dateText.text = completionDate;

        //buttonマネージャーを取得
        buttonManager = GameObject.Find("ButtonManager");

        //Edit buttonに関数割り付け
        goEditButton.onClick.AddListener(buttonManager.GetComponent<ButtonManager>().GoToEditMenu);

        //complete buttonに関数割り付け
        completedButton.onClick.AddListener(() => buttonManager.GetComponent<ButtonManager>().CompleteTask(id));
    }
}
