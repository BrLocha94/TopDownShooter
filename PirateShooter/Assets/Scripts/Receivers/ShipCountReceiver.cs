using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public sealed class UnityIntEvent : UnityEvent<int> { }

public sealed class ShipCountReceiver : ReceiverBase<int>
{
    [SerializeField]
    private UnityIntEvent onReceive;

    protected override void RegisterReceiver()
    {
        PersistentData.onShipDestroyedEvent += OnReceiveUpdate;
        OnReceiveUpdate(PersistentData.shipsDestroyed);
    }

    protected override void UnregisterReceiver()
    {
        PersistentData.onShipDestroyedEvent -= OnReceiveUpdate;
    }

    protected override void OnReceiveUpdate(int param)
    {
        onReceive?.Invoke(param);
    }
}
