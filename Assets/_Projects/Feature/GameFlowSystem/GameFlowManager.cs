using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instance = null; // 메모리 할당 변수

    [SerializeField] private GameObject battleTurnView;
    [SerializeField] private GameObject playerTurnView;
    [SerializeField] private GameObject enemyTurnView;

    private void Awake() // singleton pattern
    {
        instance = this;
    }

    void Start()
    {
        GameManager.Instance.OnStartGame += OnStartGame;
        PlayerStateBridge.OnAllocateComplete += PlayerStateBridge_OnAllocateComplete;
        EnemyStateBridge.OnAllocateComplete += EnemyStateBridge_OnAllocateComplete;
        BattleManager.Instance.OnBattleComplete += OnBattleComplete;
    }

    private void OnStartGame()
    {
        PlayerStateBridge.StartAllocate();

        Instantiate(playerTurnView);
    }

    private void PlayerStateBridge_OnAllocateComplete()
    {
        EnemyStateBridge.StartAllocate();

        Instantiate(enemyTurnView);
    }

    private void EnemyStateBridge_OnAllocateComplete()
    {
        BattleManager.Instance.StartBattle();

        Instantiate(battleTurnView);
    }

    private void OnBattleComplete()
    {
        PlayerStateBridge.StartAllocate();

        Instantiate(playerTurnView);
    }

    public void Victory()
    {
        
        SceneController.LoadScene(SceneType.SucessScene);
        AudioManager.Instance.PlaySFX("1-3"); // SFX

    }

    public void Defeat()
    {
        SceneController.LoadScene(SceneType.DefeatScene);
        AudioManager.Instance.PlaySFX("1-4"); // SFX

    }

    // Update is called once per frame
    void Update()
    {
    }
}
