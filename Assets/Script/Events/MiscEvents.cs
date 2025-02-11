using UnityEngine;
using System;

public class MiscEvents
{
    public event Action onValidedTask;

    public void ValidedTask()
    {
        onValidedTask?.Invoke();
    }
   
}
