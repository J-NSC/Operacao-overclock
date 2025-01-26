using System;
using TMPro;
using UnityEngine;

public class HUDManage : MonoBehaviour
{
    public GameObject taskPanel;
    public TMP_Text descriptionText;


    // private void OnEnable()
    // {
    //     TasktManager.OnTaskStarted += ShowQuest;
    //     TaskTrigger.OnTaskTriggered += ShowQuest;
    // }
    //
    // private void OnDisable()
    // {
    //     TasktManager.OnTaskStarted -= ShowQuest;
    //     TaskTrigger.OnTaskTriggered -= ShowQuest;
    // }
    //
    // private void Start()
    // {
    //     taskPanel.SetActive(false);
    // }
    //
    // private void ShowQuest(TaskData obj)
    // {
    //     taskPanel.SetActive(true);
    //     descriptionText.text = obj.description;
    // }

    public void AcceptTask()
    {
       Debug.Log("Accept Task");
    }

    public void RejectTask()
    {
        Debug.Log("Reject Task");
    }
}
