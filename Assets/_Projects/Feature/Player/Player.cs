using System;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    private int exchangeStack = 0;
    public int ExchangeStack => exchangeStack;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnStartGame += OnStartGame;
    }

    private void OnStartGame()
    {

    }

    public void StartAllocate()
    {
        exchangeStack++;

        if (HandManager.Instance.PlayerHands.Count <= 0)
            HandManager.Instance.InitializePlayerHands();
    }

    public void OnAllocated()
    {
        bool bIsFull = true;
        foreach(var item in FieldManager.Instance.PlayerFieldSlotViews)
        {
            if(item.CardView == null)
            {
                bIsFull = false;
                break;
            }
        }

        if (bIsFull)
        {
            exchangeStack--;
            if (exchangeStack <= 0)
            {
                EndAllocate();
            }
        }
    }

    private void EndAllocate()
    {
        PlayerStateBridge.AllocateComplete();
    }
}
