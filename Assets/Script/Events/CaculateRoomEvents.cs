using System;
using UnityEngine;

public class CaculateRoomEvents
{
    public event Action<string[]> SendedDatas;

    public void SendData(string[] data)
    {
        SendedDatas?.Invoke(data);
    }


    public event Action ShowedRoomUi;

    public void ShoweoomUi()
    {
        ShowedRoomUi?.Invoke();
    }
}
