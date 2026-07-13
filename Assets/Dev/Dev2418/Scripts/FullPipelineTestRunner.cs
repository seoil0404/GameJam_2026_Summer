using UnityEngine;

public class FullPipelineTestRunner : MonoBehaviour
{
    private void Start()
    {
        // 1. 필요한 매니저들 준비
        if (CardManager.Instance == null) gameObject.AddComponent<CardManager>();
        if (HandManager.Instance == null) gameObject.AddComponent<HandManager>();
        if (FieldManager.Instance == null) gameObject.AddComponent<FieldManager>();
        if (Enemy.Instance == null) gameObject.AddComponent<Enemy>();
        if (BattleManager.Instance == null) gameObject.AddComponent<BattleManager>();

        Field playerField = gameObject.AddComponent<Field>();
        Field enemyField = gameObject.AddComponent<Field>();
        FieldManager.Instance.PlayerField = playerField;
        FieldManager.Instance.EnemyField = enemyField;

        // 2. Player 로직이 아직 없으니, 테스트용으로 플레이어 필드도 직접 채워줌
        var dummyPlayerCards = CardManager.Instance.GenerateCards(4);
        for (int i = 0; i < 4; i++)
            playerField.Cards[i] = dummyPlayerCards[i];

        BattleManager.Instance.OnBattleComplete += () => Debug.Log("[테스트] 전투 완료 신호 받음!");

        // 3. 딱 한 번씩만 순서대로 수동 호출 (자동 루프는 안 씀)
        Debug.Log("[테스트] === 적 배치 ===");
        EnemyStateBridge.StartAllocate();

        Debug.Log("[테스트] === 전투 ===");
        BattleManager.Instance.StartBattle();

        LogFieldState("전투 후 플레이어 필드", playerField);
        LogFieldState("전투 후 적 필드", enemyField);
    }

    private void LogFieldState(string label, Field field)
    {
        for (int i = 0; i < field.Cards.Length; i++)
        {
            CardData card = field.Cards[i];
            string info = card == null ? "null" : $"{card.CombatAttribute}/{card.EffectActivateCondition}";
            Debug.Log($"[테스트] {label}[{i}] = {info}");
        }
    }
}