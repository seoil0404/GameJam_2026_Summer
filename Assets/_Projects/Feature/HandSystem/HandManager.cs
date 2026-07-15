using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    [SerializeField] private int cardGenerateCount = 8;

    [SerializeField] private HandView playerHandView;
    [SerializeField] private EnemyStateView enemyStateView;
    public HandView PlayerHandView => playerHandView;
    private List<CardData> _PlayerHands = new();
    public List<CardData> PlayerHands
    { 
        get
        {
            if (_PlayerHands.Count <= 0)
            {
                StartCoroutine(InitializePlayerHands());
            }

            return _PlayerHands;
        }
        set
        {
            _PlayerHands = value;
        }
    }

    [SerializeField] private HandView enemyHandView;
    public HandView EnemyHandView => enemyHandView;
    private List<CardData> _EnemyHands = new();
    public List<CardData> EnemyHands
    {
        get
        {
            if (_EnemyHands.Count <= 0)
                _EnemyHands = CardManager.Instance.GenerateCards(cardGenerateCount, OwnerType.Enemy);

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

    private void Start()
    {
        GameManager.Instance.OnStartGame += OnStartgame;
    }

    private void OnStartgame()
    {
        if(_EnemyHands.Count <= 0)
            StartCoroutine(InitializeEnemyHands());
        if(_PlayerHands.Count <= 0)
            StartCoroutine(InitializePlayerHands());
    }

    public IEnumerator InitializePlayerHands()
    {
        _PlayerHands = CardManager.Instance.GenerateCards(cardGenerateCount, OwnerType.Player);
        foreach (CardData card in _PlayerHands)
        {
            var cardView = CardManager.Instance.GenerateCardView(card);
            cardView.AttatchCard(playerHandView);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator InitializeEnemyHands()
    {
        _EnemyHands = CardManager.Instance.GenerateCards(cardGenerateCount, OwnerType.Enemy);
        foreach (CardData card in _EnemyHands)
        {
            var cardView = CardManager.Instance.GenerateCardView(card);
            cardView.AttatchCard(enemyHandView);

            yield return new WaitForSeconds(0.1f);
        }

        int fireCount = 0;
        int waterCount = 0;
        int grassCount = 0;

        foreach (var cardData in EnemyHands)
        {
            switch (cardData.CombatAttribute)
            {
                case CombatAttribute.Fire:
                    fireCount++;
                    break;
                case CombatAttribute.Water:
                    waterCount++;
                    break;
                case CombatAttribute.Grass:
                    grassCount++;
                    break;
                default:
                    break;
            }
        }

        enemyStateView.SetView(fireCount, waterCount, grassCount);
    }
}
