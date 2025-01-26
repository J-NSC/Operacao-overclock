using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestInfoSO info;
    
    public QuestState state;
    
    int currentQuestStepIndex = 0;

    public Quest(QuestInfoSO info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepsPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = getCurrentQuestStepPrefab();

        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    public GameObject getCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepsPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Quest Step Prefab Not Found" + info.Id + " : " + currentQuestStepIndex);
        }
        
        return questStepPrefab;
    }
}
