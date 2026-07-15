using System;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    [SerializeField] private HealthView healthView;
    [SerializeField] private ExchangeStackView exchangeStackView;

    private int exchangeStack = 0;
    public int ExchangeStack => exchangeStack;

    public override int Health
    {
        get
        {
            return base.Health;
        }
        set
        {
            base.Health = value < 0 ? 0 : value;
            if(base.Health == 0)
            {
                GameFlowManager.instance.Defeat();
            }

            healthView.SetHealthText(value);
        }
    }

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
        exchangeStackView.SetStackView(exchangeStack);

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
            exchangeStackView.SetStackView(exchangeStack);

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
