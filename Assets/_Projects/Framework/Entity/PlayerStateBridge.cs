using System;
using UnityEngine;

public static class PlayerStateBridge
{
    public static event Action OnAllocateComplete;
    public static bool bIsAllocating { get; private set; } = false;

    public static Entity GetPlayer()
    {
        return Player.Instance;
    }

    public static void StartAllocate()
    {
        bIsAllocating = true;

        Player.Instance.StartAllocate();
    }

    public static void AllocateComplete()
    {
        bIsAllocating = false;

        CardVisualSynchronyzer.Instance.SyncPlayer();

        OnAllocateComplete?.Invoke();
    }
}