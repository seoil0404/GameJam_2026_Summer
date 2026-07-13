using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    [SerializeField] private int cardGenerateCount = 8;

    private List<CardData> _PlayerHands = new();
    public List<CardData> PlayerHands
    { 
        get
        {
            if (_PlayerHands.Count <= 0)
                _PlayerHands = CardManager.Instance.GenerateCards(cardGenerateCount);

            return _PlayerHands;
        }
        set
        {
            _PlayerHands = value;
        }
    }

    private List<CardData> _EnemyHands = new();
    public List<CardData> EnemyHands
    {
        get
        {
            if (_EnemyHands.Count <= 0)
                _EnemyHands = CardManager.Instance.GenerateCards(cardGenerateCount);

            return _EnemyHands;
        }
        set
        {
            _EnemyHands = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
