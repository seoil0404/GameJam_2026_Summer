using UnityEngine;

public class EnemyStateBtidgeTestRunner : MonoBehaviour
{
    private void Start()
    {
        
        if (HandManager.Instance == null)
            gameObject.AddComponent<HandManager>();

        if (FieldManager.Instance == null)
            gameObject.AddComponent<FieldManager>();

        Field enemyField = gameObject.AddComponent<Field>();
        enemyFieldCache = enemyField;
        FieldManager.Instance.EnemyField = enemyField;

        Enemy enemy = gameObject.AddComponent<Enemy>();

        
        HandManager.Instance.EnemyHands = enemy.GenerateRandomDeck(10);
        Debug.Log($"[테스트] 초기 손패 개수: {HandManager.Instance.EnemyHands.Count}");

        
        EnemyStateBridge.OnAllocateComplete += () => Debug.Log("[테스트] 배치 완료 신호 받음!");

        
        EnemyStateBridge.StartAllocate();
        LogFieldState("1라운드 배치 후");

        
        EnemyStateBridge.StartAllocate();
        LogFieldState("2라운드 배치 후");
    }

    private void LogFieldState(string label)
    {
        Debug.Log($"[테스트] {label} - 남은 손패 개수: {HandManager.Instance.EnemyHands.Count}");

        for (int i = 0; i < enemyFieldCache.Cards.Length; i++)
        {
            CardData card = enemyFieldCache.Cards[i];
            string info = card == null ? "null" : $"{card.CombatAttribute}/{card.EffectActivateCondition}";
            Debug.Log($"[테스트] Field[{i}] = {info}");
        }
    }

    private Field enemyFieldCache;
}
