using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnStartGame;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }


    public void OnEndGame(GameResult gameResult)
    {

    }

    public struct GameResult
    {
        public bool IsWin {  get; private set; }

        public GameResult(bool bIsWin)
        {
            IsWin = bIsWin;
        }
    }
}
