using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    public List<CardData> PlayerHands { get; set; } = new();
    public List<CardData> EnemyHands { get; set; } = new();

    private void Awake()
    {
        Instance = this;
    }
}
