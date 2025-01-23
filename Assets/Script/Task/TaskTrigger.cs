using System;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] private TaskData taskData;

    private void OnMouseDown()
    {
        QuestManager.instance.StartTask(taskData);
    }
}
