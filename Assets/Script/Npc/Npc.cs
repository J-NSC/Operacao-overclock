using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventsManager.instance.caculateRoomEvents.ShoweoomUi();
        }
    }
    
    
}
