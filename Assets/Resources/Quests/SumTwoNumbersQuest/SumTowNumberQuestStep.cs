using UnityEngine;

public class SumTowNumberQuestStep : QuestStep
{

    int number_1 = 0 ;
    int number_2 = 0 ;
    public int result;

    void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onValidedTask += ValideteSum;
    }
   
    void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onValidedTask -= ValideteSum;
    }
    
    void ValideteSum()
    {
        FinishQuestStep();
        //
        // if (number_1 + number_2 == result)
        // {
        // }
        // else
        // {
        //     //TODO Mostrar avisio de soma errada
        // }
    }
    
    protected override void SetQuestStepState(string state)
    {
        throw new System.NotImplementedException();
    }
}
