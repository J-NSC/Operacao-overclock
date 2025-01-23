using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public static event Action<TaskData> OnTaskStarted;
    public static event Action<bool> OnTaskCompleted;
    
    private TaskData taskData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartTask(TaskData taskData)
    {
        this.taskData = taskData;
        
        OnTaskStarted?.Invoke(taskData);
    }

    public void SubmitAnswer(string playerAnswer)
    {
        if(taskData == null) return;
        
        bool isCorrect = playerAnswer == taskData.correctAnswer;
        
        
        OnTaskCompleted?.Invoke(isCorrect);
    }
}
