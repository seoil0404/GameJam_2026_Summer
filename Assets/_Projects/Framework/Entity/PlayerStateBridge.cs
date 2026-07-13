using System;
using UnityEngine;

public static class PlayerStateBridge
{
    public static event Action OnAllocateComplete;

    private static bool isInitialized = false;

    public static Entity GetPlayer()
    {
        return null;
    }

    public static void StartAllocate()
    {
        CardManager.Instance.ActiveAllCardViews();
    }

    private static void AllocateComplete()
    {
        CardVisualSynchrolyzer.Instance.SyncPlayer();
        CardManager.Instance.DeactiveAllCardViews();

        OnAllocateComplete?.Invoke();
    }
}