using UnityEngine;

public class CharacterAbilityTestRunner : MonoBehaviour
{
    [SerializeField] private CharacterData nolbu;
    [SerializeField] private CharacterData geomeori;
    [SerializeField] private CharacterData jackpot;

    private void Start()
    {
        SetupManagers();

        TestNolbu();
        TestJackpot();
        TestGeomeori();
    }

    private void SetupManagers()
    {
        if (CardManager.Instance == null) gameObject.AddComponent<CardManager>();
        if (HandManager.Instance == null) gameObject.AddComponent<HandManager>();
        if (FieldManager.Instance == null) gameObject.AddComponent<FieldManager>();
        if (GameManager.Instance == null) gameObject.AddComponent<GameManager>();
        if (Player.Instance == null) gameObject.AddComponent<Player>();
        if (Enemy.Instance == null) gameObject.AddComponent<Enemy>();
        if (BattleManager.Instance == null) gameObject.AddComponent<BattleManager>();
        if (GetComponent<CharacterAbilityManager>() == null) gameObject.AddComponent<CharacterAbilityManager>();

        if (FieldManager.Instance.PlayerField == null)
            FieldManager.Instance.PlayerField = gameObject.AddComponent<Field>();
        if (FieldManager.Instance.EnemyField == null)
            FieldManager.Instance.EnemyField = gameObject.AddComponent<Field>();
    }

    private void TestNolbu()
    {
        CharacterSelection.Select(nolbu);
        HandManager.Instance.InitializePlayerHands();
        Debug.Log($"[놀부 테스트] 손패 개수: {HandManager.Instance.PlayerHands.Count} (10장이면 정상)");
    }

    private void TestJackpot()
    {
        CharacterSelection.Select(jackpot);

        int trials = 1000;
        int doubleCount = 0;

        for (int i = 0; i < trials; i++)
        {
            if (CharacterAbilityUtility.ApplyJackpotChance(10) == 20)
                doubleCount++;
        }

        Debug.Log($"[잭팟 테스트] {trials}번 중 {doubleCount}번 2배 발동 (200번 근처면 정상, 확률이라 정확히 200은 아닐 수 있음)");
    }

    private void TestGeomeori()
    {
        CharacterSelection.Select(geomeori);

        Player.Instance.Health = 5;
        Enemy.Instance.Health = 100;

        for (int turn = 1; turn <= 3; turn++)
        {
            var playerCards = CardManager.Instance.GenerateCards(4, OwnerType.Player);
            var enemyCards = CardManager.Instance.GenerateCards(4, OwnerType.Enemy);

            for (int i = 0; i < 4; i++)
            {
                FieldManager.Instance.PlayerField.Cards[i] = playerCards[i];
                FieldManager.Instance.EnemyField.Cards[i] = enemyCards[i];
            }

            BattleManager.Instance.StartBattle();
            Debug.Log($"[거머리 테스트] {turn}턴 후 플레이어 체력: {Player.Instance.Health}");
        }
    }
}