using System;
using UnityEngine;

public static class PlayerStateBridge
{
    public static event Action OnAllocateComplete;

    private static bool isInitialized = false;

    public static Entity GetPlayer()
    {
        return Player.Instance;
    }

    public static void StartAllocate()
    {
        CardManager.Instance.ActiveAllCardViews();
        Player.Instance.StartAllocate();
    }

    public static void AllocateComplete()
    {
        CardVisualSynchrolyzer.Instance.SyncPlayer();
        CardManager.Instance.DeactiveAllCardViews();

        OnAllocateComplete?.Invoke();
    }
}