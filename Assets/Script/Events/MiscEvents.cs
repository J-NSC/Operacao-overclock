using UnityEngine;
using System;

public class MiscEvents
{
    public event Action onCoinCollected;

    public void CoinsCollected()
    {
        onCoinCollected?.Invoke();
    }
    
    public event Action onGemCollected;

    public void GemsCollected()
    {
        onCoinCollected?.Invoke();
    }
}
