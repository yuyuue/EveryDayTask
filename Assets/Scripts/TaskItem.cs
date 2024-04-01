using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    //�^�X�N���̗v�f���C���X�y�N�^�[�Őݒ�
    public TextMeshProUGUI nameText;    //�^�X�N��
    public TextMeshProUGUI countText;   //�B����
    public TextMeshProUGUI dateText;    //�ŏI�B�����t
    public Button completedButton;      //�B���{�^��
    public Button goEditButton;         //Edit button

    //�}�l�[�W���[�v�f��錾
    public GameObject buttonManager;    //button �}�l�[�W���[

    //�����̃^�X�N�̏��
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

        //button�}�l�[�W���[���擾
        buttonManager = GameObject.Find("ButtonManager");

        //Edit button�Ɋ֐�����t��
        goEditButton.onClick.AddListener(buttonManager.GetComponent<ButtonManager>().GoToEditMenu);

        //complete button�Ɋ֐�����t��
        completedButton.onClick.AddListener(() => buttonManager.GetComponent<ButtonManager>().CompleteTask(id));
    }
}
