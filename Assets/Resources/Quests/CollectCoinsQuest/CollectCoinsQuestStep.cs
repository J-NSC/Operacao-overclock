using System;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
   int coinsCollected = 0;
   int coinsToComplete = 5;


   void OnEnable()
   {
      GameEventsManager.instance.miscEvents.onCoinCollected += CoinCollected;
   }
   
   void OnDisable()
   {
      GameEventsManager.instance.miscEvents.onCoinCollected -= CoinCollected;
   }

   void CoinCollected()
   {
      if (coinsCollected < coinsToComplete )
      {
         coinsCollected++;
      }

      if (coinsCollected >= coinsToComplete)
      {
         FinishQuestStep(); 
      }
   }
}
