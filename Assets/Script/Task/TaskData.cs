using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "Taks/TaskData")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public string description;
    public string correctAnswer;
}
