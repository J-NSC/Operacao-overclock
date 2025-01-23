using System;
using TMPro;
using UnityEngine;

public class HUDManage : MonoBehaviour
{
    public GameObject taskPanel;
    public TMP_Text descriptionText;


    private void OnEnable()
    {
        QuestManager.OnTaskStarted += ShowQuest;
    }
    
    private void OnDisable()
    {
        QuestManager.OnTaskStarted -= ShowQuest;
    }

    private void ShowQuest(TaskData obj)
    {
        descriptionText.text = obj.description;
    }
}
