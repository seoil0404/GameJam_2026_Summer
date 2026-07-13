using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnStartGame;

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
