using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    bool isFineshed = false;

    protected void FinishQuestStep()
    {
        if (!isFineshed)
        {
            isFineshed = true;
            
            Destroy(this.gameObject);
        } 
    }
}
