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
        exchangeStack = 4;
    }

    public void StartAllocate()
    {
        exchangeStack++;
    }

    public void OnAllocated()
    {
        exchangeStack--;
        if(exchangeStack <= 0)
        {
            EndAllocate();
        }
    }

    private void EndAllocate()
    {
        PlayerStateBridge.AllocateComplete();
    }
}
